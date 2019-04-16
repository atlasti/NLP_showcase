require 'date'
require 'time'
require 'fileutils'
require 'ci/reporter/rake/test_unit' # sudo gem install ci_reporter_test_unit
require 'erb'
require 'json'
require 'erb'
require 'rexml/document'
include REXML

Encoding.default_external = Encoding::UTF_8
Encoding.default_internal = Encoding::UTF_8

DOT_NET = `which dotnet`.strip
if DOT_NET.length == 0
  puts 'Could not find dotnet, please install from https://aka.ms/dotnet-download'
  exit -1
end
DOT_NET_VERSION = `dotnet --version`.strip
unless DOT_NET_VERSION.start_with? '2.2.'
  puts "Expecting at least version 2.2 for dotnet, please update from https://aka.ms/dotnet-download"
  exit -1
end

PROTOC = `which protoc`.strip
if PROTOC.length == 0
  put 'Could not find grpc, please install (brew install grpc)'
  exit -1
end

POD = `which pod`.strip
if POD.length == 0
  put 'Could not find cocoa pods, please install (sudo gem install cocoapods)'
  exit -1
end

XCPRETTY = `which xcpretty`.strip
if XCPRETTY.length == 0
  puts "Please install xcpretty (sudo gem install xcpretty) and try again."
  exit -1
end

MAC_APP_DIR = File.expand_path('Objective-C')
PODS_DIR = File.expand_path("#{MAC_APP_DIR}/Pods")
KEYCHAIN = "#{ENV['HOME']}/Library/Keychains/developer.keychain-db"
BASE_DIR = `pwd`.strip

DISTRIBUTION_GROUP='Developers'
APP_CENTER_API_TOKEN='b2aa4b2f8f015cad744ceaf05a3f01731ba4ee03'

# Determine latest installed Xcode version
xcode_build_subpath = '/Contents/Developer/usr/bin/xcodebuild'
xcode_max_version = 0
Dir.glob("/Applications/Xcode*.app").each do |xcode_app|
  xcode_build = "#{xcode_app}#{xcode_build_subpath}"
  version = `"#{xcode_build}" -version|head -1|cut -f2 -d' '|cut -f1 -d'.'`.strip.to_i
  if version > xcode_max_version 
    xcode_max_version = version
    @build_cmd = xcode_build
  end
end
@xcodebuild_counter = 0

JOB_NAME = ENV['JOB_NAME'] || 'test'
BUILD_NUMBER = ENV['BUILD_NUMBER'].to_i || Time.new.to_i
TEMP_DIR = "#{ENV['TMPDIR']}#{ENV['USER']}/#{JOB_NAME}_b#{BUILD_NUMBER}"
ARCHIVE_PATH = "#{TEMP_DIR}/HelloWorldApp.xcarchive"
FileUtils::mkdir_p TEMP_DIR unless File.exist?(TEMP_DIR)

CURRENT_DIR=Dir.pwd
TEST_REPORTS="#{CURRENT_DIR}/test/reports"

def unlock_keychain
  if File.exists?(KEYCHAIN)
    puts "Unlocking keychain at #{KEYCHAIN}"
    `security list-keychains -s \"#{KEYCHAIN}\"`
    `security default-keychain -s \"#{KEYCHAIN}\"`
    `security unlock-keychain -p 'By4aci#s6679' \"#{KEYCHAIN}\"`
  else
    puts "Could not find a keychain to unlock at location #{KEYCHAIN}."
  end
end

def run_system(command)
  puts "🐚 #{command}"
  started = Time.now
  successful = system(command)
  puts "⏳ Took #{Time.at(Time.now - started).gmtime.strftime("%H:%M:%S")}" if Time.now - started > 1.0
  return if command['xcodebuild'] != nil 
  abort "\n\nFailed to execute command:\n#{command}" if !successful || $?.exitstatus != 0
end

def xcodebuild(task, project)
  command = task
  command = 'test' if task == 'integration_test'
  isTesting = command == 'test'
  isIntegrationTesting = task == 'integration_test'
  isArchiving = command == 'archive'
  isAnalyzing = command == 'analyze'

  configuration = 'Release'

  if project == 'HelloWorldManager'
    xcodebuild_project = "-workspace #{MAC_APP_DIR}/#{project}.xcworkspace"
    scheme = 'HelloWorldManagerFramework'
  elsif project == 'HelloWorldApp'
    xcodebuild_project = "-project #{MAC_APP_DIR}/#{project}.xcodeproj"
    scheme = project
  end

  info_plist = "#{MAC_APP_DIR}/#{project}/Info.plist"
  current_build_number = `/usr/libexec/PlistBuddy -c "Print CFBundleVersion" '#{info_plist}'`.to_i
  run_system("/usr/libexec/PlistBuddy -c 'Set :CFBundleVersion #{BUILD_NUMBER}' \"#{info_plist}\"") if current_build_number != BUILD_NUMBER

  @xcodebuild_counter += 1
  output_files_prefix = "#{TEMP_DIR}/xcodebuild_#{@xcodebuild_counter}"
  error_output = "#{output_files_prefix}.err"
  unformatted_output = "#{output_files_prefix}.out"
  formatted_output = "#{output_files_prefix}.log"
  derived_data = "#{TEMP_DIR}/derivedData"

  build_command = "'#{@build_cmd}' #{command} -derivedDataPath '#{derived_data}' #{xcodebuild_project} -scheme '#{scheme}' -configuration #{configuration}"
  build_command << " -archivePath '#{ARCHIVE_PATH}' " if isArchiving
  build_command << " 2>#{error_output} | tee '#{unformatted_output}' | "
  build_command << "#{XCPRETTY} --no-color >'#{formatted_output}'"
  build_command << " && exit ${PIPESTATUS[0]}"

  run_system build_command

  # xcodebuild returns success even when there were problems
  failed_output=""

  failed_output = `egrep -A 1 -B 4 'clang: error:' "#{unformatted_output}"` if failed_output.length == 0
  failed_output = `egrep -B 6 ' error generated.' "#{unformatted_output}"` if failed_output.length == 0
  if isTesting
    failed_output = `sed -n '/Failing tests:/,/ TEST FAILED /p' "#{error_output}"` if failed_output.length == 0
  end
  if isAnalyzing
    failed_output = `egrep -B 2 ': warning: ' "#{unformatted_output}"` if failed_output.length == 0
  end
  failed_output = `egrep -C 4 'The following build commands failed' "#{error_output}"` if failed_output.length == 0
  error_regex = '(error|errors) generated.'
  failed_output = `egrep -B 4 '#{error_regex}' "#{unformatted_output}"` if failed_output.length == 0
  failed_output << "\n" << `egrep -C 4 '(Code Sign error:|Code signing is required for product)' "#{unformatted_output}"`
  failed_output.strip!

  if failed_output.length > 0
    shortened_output = ""
    failed_output.split("\n").each do |line|
      line.gsub!(TEMP_DIR, '$TMP')
      line.gsub!(BASE_DIR, '.')
#      if line.length > 160 && ! line.include?("\^~~") && ! line.include?("error:")
#        line = "#{line[0..30]} … #{line[-130..-1]}"
#      end
      shortened_output << line << "\n"
    end

    puts "\n\n⛔️ ##################### xcodebuild error output #####################:\n#{shortened_output}"
    abort "\n\n🤯 Failed to call command:\n#{command}"
  end
end

def set_nuget_version(config_file, version_number)
  # Update Nuget repo pointing to submodule 
  unless File.exists? config_file
    puts "Could not open #{config_file}"
    exit -1
  end
  xmldoc = Document.new(File.new(config_file))
  XPath.each(xmldoc, "/Project/PropertyGroup/Version") do |node|
    node.text = version_number
  end
  formatter = REXML::Formatters::Pretty.new
  formatter.compact = true
  File.open(config_file,'w') { |file| file.puts formatter.write(xmldoc.root, "") }
end

desc 'Run tests'
task :test => [:clean, :prepare_dotnet] do
  # https://stackoverflow.com/questions/43284881/jenkins-integration-for-dotnet-test#44121352
  run_system("#{DOT_NET} test Tests/Tests.csproj --results-directory test-results --logger 'trx;LogFileName=results.trx'")
end

desc 'Clean build folder'
task :clean do
  FileUtils.rm_rf('./ATLASCore/bin')
  FileUtils.rm_rf('./TextServer/bin')
  FileUtils.rm_rf('./Tests/bin')
end

desc 'Build the app, still required for unit tests'
task :build_app do
  run_system("#{DOT_NET} publish TextServer/TextServer.csproj --self-contained true -r osx-x64 -c Release")
end

desc 'Archive'
task :archive do
  unlock_keychain

  xcodebuild('archive', 'HelloWorldApp')
end

task :testunit => 'ci:setup:testunit' do
  FileUtils::mkdir_p TEST_REPORTS unless File.exist?(TEST_REPORTS)
end

desc 'Prepare dotnet dependencies'
task :prepare_dotnet do
  run_system("#{DOT_NET} restore")
  run_system("#{DOT_NET} list package")
end

desc 'Prepare environment for Objective-C'
task :prepare_objc do
  puts "#{Time.now.strftime("%H:%M:%S,%L")} 👩‍🔧 Preparing environment..."

  # Update generated Protobuf sourcefiles
  proto_file = './HelloWorld.proto'
  target_dir = 'HelloWorldServer/SSD.ATLASti.Common/HelloWorld/Model'
  target_file = "#{target_dir}/HelloWorld.cs"
  proto_needs_update = !File.exists?(target_file) || File.mtime(proto_file) > File.mtime(target_file)
  if proto_needs_update
    run_system("#{PROTOC} -I. --csharp_out '#{target_dir}' --grpc_out 'HelloWorldServer/SSD.ATLASti.Common/HelloWorld/Server' #{proto_file} --plugin=protoc-gen-grpc=/usr/local/bin/grpc_csharp_plugin")
  end

  # Update Cocoapods defintions
  pod_needs_update = proto_needs_update
  if pod_needs_update
    Dir.chdir MAC_APP_DIR do
      File.delete 'Podfile.lock' if File.exists? 'Podfile.lock'
      update_repo = File.exists?(PODS_DIR) && ((Time.now - File.mtime(PODS_DIR))/(24*60*60)).to_i > 0
      FileUtils.rm_rf PODS_DIR if Dir.exists? PODS_DIR
      FileUtils.rm_rf 'HelloWorldManager.xcworkspace' if Dir.exists? 'HelloWorldManager.xcworkspace'
      run_system("#{POD} install #{update_repo ? '--repo-update' : ''}")
    end
  end

  puts "#{Time.now.strftime("%H:%M:%S,%L")} 🍱 Everything is set up properly."
end

task :prepare => [:prepare_dotnet, :prepare_objc, :build_helloworldserver]

desc 'Build HelloWorld Server'
task :build_helloworldserver do
  run_system("#{DOT_NET} publish HelloWorldServer/HelloWorldServer.csproj --self-contained true -r osx-x64 -c Release")
end

desc 'Build HelloWorldManager framework'
task :build_HelloWorldManager do
  xcodebuild('build', 'HelloWorldManager')
  run_system("rsync -a --checksum --delete #{TEMP_DIR}/")
end

desc 'Build HelloWorldApp app'
task :build_HelloWorldApp => [:prepare_objc, :build_textserver, :archive] do
  archive_app = "#{ARCHIVE_PATH}/Products/Applications/HelloWorldApp.app"
  @target_zip_name = "#{TEMP_DIR}/HelloWorldApp.app.zip"
  run_system("ditto -c -k --sequesterRsrc --keepParent '#{archive_app}' '#{@target_zip_name}'")
  @target_dsym_zip_name = "#{TEMP_DIR}/HelloWorldApp.app.dSYM.zip"
  run_system("ditto -c -k --sequesterRsrc --keepParent '#{ARCHIVE_PATH}/dSYMs/HelloWorldApp.app.dSYM' '#{@target_dsym_zip_name}'")
  puts "The app can be found at #{@target_zip_name}"
  @changelog = 'New version'
end

task :default => [:build_textserver]