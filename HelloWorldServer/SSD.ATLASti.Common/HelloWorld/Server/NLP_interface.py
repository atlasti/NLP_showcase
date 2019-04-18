import argparse
import NLP

parser = argparse.ArgumentParser()
NLP_group = parser.add_mutually_exclusive_group()
NLP_group.add_argument("-NER", action="store_true", help="Named entity recognition")
NLP_group.add_argument("-LS", help="searches the lemmatized document for the lemmatized input string")
NLP_group.add_argument("-L", action="store_true", help="returns lemmas and their frequencies")
parser.add_argument("document", help="path to document")
args = parser.parse_args()

file_name = args.document
file = open(file_name, "r")
document = file.read()

if args.LS:
    sentences = NLP.lemma_sentences_for_unionised_request(search_term = args.LS, document = document)
    print(sentences)
elif args.L:
    lemmas_and_frequencies = NLP.lemmas_for_document(document = document)
    print(lemmas_and_frequencies)
elif args.NER:
    positions_and_tags = NLP.named_entity_sentences_and_types_for_document(document)
    print(positions_and_tags)