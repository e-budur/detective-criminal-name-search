package com.detective.index;

import java.io.IOException;

import org.apache.lucene.index.IndexWriter;
import org.apache.lucene.index.IndexWriterConfig;
import org.apache.lucene.store.Directory;

public class BKIndexWriter extends IndexWriter{

	public BKIndexWriter(Directory d, IndexWriterConfig conf)
			throws IOException {
		super(d, conf);
		// TODO Auto-generated constructor stub
	}

}
