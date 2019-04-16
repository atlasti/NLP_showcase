import spacy


def lemmas_for_document(self, document):
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

def lemma_sentences_for_unionised_request(self, search_term, document):
	print('spacy lematizer received request')
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
	#TODO: Using sentence.start and sentence.end yields the start and end tokens, not positions...
	for sent in sentences:
		token = doc[sent.end - 1]
		print('Sentence start: ' + str(doc[sent.start].idx) + ' | Sentence end: ' + str(token.idx + len(token)))
	return sentences
