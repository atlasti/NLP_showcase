import spacy


def lemmas_for_document(document):
    #document = (u'I am')
    nlp = spacy.load('en_core_web_sm')
    doc = nlp(document)

    lemmas = u''
    lemmas_and_frequencies_dict = {}

    for token in doc:
        lemmas = lemmas + token.lemma_
        if token.lemma_ in lemmas_and_frequencies_dict:
            lemmas_and_frequencies_dict[token.lemma_] = lemmas_and_frequencies_dict[token.lemma_] + 1
        else:
            lemmas_and_frequencies_dict[token.lemma_] = 1

    # Create two lists/sets from the dictionary
    lemmas = list(lemmas_and_frequencies_dict)
    frequencies = list(lemmas_and_frequencies_dict.values())
    lemmas_and_frequencies = lemmas, frequencies
    return lemmas_and_frequencies

def lemma_sentences_for_unionised_request(search_term, document):
    sentences = set()
    #simple solution
    nlp = spacy.load('en_core_web_sm')
    
    sT = nlp(search_term)
    search_term_lemmas = set()
    for token in sT:
        search_term_lemmas.add(token.lemma_)

    doc = nlp(document)
    for sentence in doc.sents:
        sentence_document = nlp(sentence.text)
        found_lemmas = 0
        for token in sentence_document:
            if token.lemma_ in search_term_lemmas:
                found_lemmas = found_lemmas + 1
            if found_lemmas == len(search_term_lemmas):
                sentences.add(sentence)
                break
    for sent in sentences:
        token = doc[sent.end - 1]
        print('Sentence start: ' + str(doc[sent.start].idx) + ' | Sentence end: ' + str(token.idx + len(token)))
    return sentences

# Returns a string that formatted as thusly: 
#sentence start UTF-16 position
#sentence end UTF-16 position
#associated tag
def named_entity_sentences_and_types_for_document(document):
    nlp = spacy.load("en_core_web_sm")
    doc = nlp(document)

    positions_and_tags = ""
    for ent in doc.ents:
        positions_and_tags = positions_and_tags + str(ent.start_char) + "\n" + str(ent.end_char) +  "\n" + ent.label_ + "\n"

    return positions_and_tags