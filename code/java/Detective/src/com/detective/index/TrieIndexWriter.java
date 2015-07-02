package com.detective.index;

import java.io.IOException;

import org.apache.lucene.document.Document;
import org.apache.lucene.index.IndexWriter;
import org.apache.lucene.index.IndexWriterConfig;
import org.apache.lucene.store.Directory;

import com.detective.misc.Utils;

public class TrieIndexWriter extends IndexWriter{

	public TrieIndexWriter(Directory arg0, IndexWriterConfig arg1)
			throws IOException {
		super(arg0, arg1);
		// TODO Auto-generated constructor stub
	}

	
	public void addDocument(Document doc){
		Utils.println(doc);
	}
	
}
