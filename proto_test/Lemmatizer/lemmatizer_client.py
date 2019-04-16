#Client for Lemmatizer of NLP for ATLAS.ti

from concurrent import futures
import time
import logging

import grpc

import Lemmatizer_pb2
import Lemmatizer_pb2_grpc

DIVIDER = '-----'

def run_test(stub):
    request = Lemmatizer_pb2_grpc.TestString(message = 'Hello')
    return stub.Test(request)

def run_lemmas_and_frequencies_for_document(stub):
    document = Lemmatizer_pb2.Document(content = u'I am.')
    response = stub.LemmasForDocument(document)

def run_lemma_sentences_for_unionised_request(stub):
    search_term =  u'I am.'
    document = Lemmatizer_pb2.Document(content = u'You are. And she is. But not this.')
    query = Lemmatizer_pb2.Query(searchTerm = search_term, document = document)
    print('client sending request')
    response = stub.LemmaSentencesForUnionisedRequest(query)
    print('client received request')
    return response

def run():
    # NOTE(gRPC Python Team): .close() is possible on a channel and should be
    # used in circumstances in which the with statement does not fit the needs
    # of the code.
    with grpc.insecure_channel('localhost:50051') as channel:
        stub = Lemmatizer_pb2_grpc.LemmatizerStub(channel)
        print(DIVIDER + ' gRPC:localhost:50051 ' + DIVIDER)
        response = run_test((stub))
        print('client received response')
        print(response)

if __name__ == '__main__':
    logging.basicConfig()
    run()