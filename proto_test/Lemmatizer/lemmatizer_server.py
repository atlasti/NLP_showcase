#Server for Lemmatizer of NLP for ATLAS.ti

from concurrent import futures
import time
import logging

import grpc

import lemmatizer_spacy
import Lemmatizer_pb2
import Lemmatizer_pb2_grpc

_ONE_DAY_IN_SECONDS = 60 * 60 * 24
DIVIDER = '-----'

class LemmatizerServicer(Lemmatizer_pb2_grpc.LemmatizerServicer):
    def LemmaSentencesForUnionisedRequest(self, request, context):
        print('server received request')
        query = request.searchTerm
        document = request.document
        lemma_sentences = lemmatizer_spacy.lemma_sentences_for_unionised_request(self, searchTerm, document)
        return lemma_sentences

    def LemmasForDocument(self, request, context):
        print('server received request')
        document = request.content
        lemmas_and_frequencies = lemmatizer_spacy.lemmas_for_document(self, document)
        return_value = Lemmatizer_pb2.LemmasAndFrequencies()
        return_value.lemma.extend(lemmas_and_frequencies[0])
        return_value.frequency.extend(lemmas_and_frequencies[1])
        return return_value

    def TEST(self, request, context):
        print('server received request')
        response = Lemmatizer_pb2_grpc.TestString(message = 'Hello')
        return response

def serve():
    print('serving')
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    Lemmatizer_pb2_grpc.add_LemmatizerServicer_to_server(
        LemmatizerServicer(), server)
    server.add_insecure_port('[::]:50051')

    server.start()
    try:
        while True:
            time.sleep(_ONE_DAY_IN_SECONDS)
    except KeyboardInterrupt:
        server.stop(0)


if __name__ == '__main__':
    logging.basicConfig()
    serve()
