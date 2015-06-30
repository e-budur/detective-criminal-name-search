//Original code: https://github.com/macluq/HelloLucene/blob/master/src/main/java/HelloLucene/HelloLucene.java

package com.detective.main;
import java.io.IOException;
import java.text.ParseException;
import java.util.Scanner;

import org.apache.lucene.analysis.standard.StandardAnalyzer;
import org.apache.lucene.document.Document;
import org.apache.lucene.document.Field;
import org.apache.lucene.document.StringField;
import org.apache.lucene.document.TextField;
import org.apache.lucene.index.DirectoryReader;
import org.apache.lucene.index.IndexReader;
import org.apache.lucene.index.IndexWriter;
import org.apache.lucene.index.IndexWriterConfig;
import org.apache.lucene.queryparser.classic.QueryParser;
import org.apache.lucene.search.IndexSearcher;
import org.apache.lucene.search.Query;
import org.apache.lucene.search.ScoreDoc;
import org.apache.lucene.search.TopScoreDocCollector;
import org.apache.lucene.store.Directory;
import org.apache.lucene.store.RAMDirectory;

import com.detective.data.SDNFileParser;
import com.detective.datamodel.SDN;
import com.detective.misc.Utils;

public class Starter {

	public static void main(String[] args) throws IOException, ParseException {
		
		if(!isValidInput(args))	
			System.exit(-1);
		
		SDNFileParser sdnFileParser = new SDNFileParser(args[0]);
		
		StandardAnalyzer analyzer = new StandardAnalyzer();

		Directory index = new RAMDirectory();

		IndexWriterConfig config = new IndexWriterConfig(analyzer);

		IndexWriter w = new IndexWriter(index, config);
		
		
		for(SDN sdn : sdnFileParser.readAllSDNs())
		{
			int nameIndex = 0;
			for(String name:sdn.getNames())
			{
				Utils.println(sdn);
				addDoc(w, name, sdn.getId()+"-"+nameIndex);
				nameIndex++;
			}
		}
		
		addDoc(w, "Priyank Mathur", "priyank");
		addDoc(w, "Vein Kong", "skongum02");
		addDoc(w, "Jacob S. Lee", "smlee729");
		addDoc(w, "Stephen Chu", "stephen");
		addDoc(w, "Emrah Budur", "e-budur");
		w.close();

		// 2. query
		Utils.println("Query:");
		String fuzzyQuery = "FIRST FUZZY QUERY CARE CANTER~";
		Utils.println(fuzzyQuery);
		Scanner s = new Scanner(System.in);
		
		while(fuzzyQuery.compareTo("exit()")!=0)
		{
			// the "name" area specifies the default field to use
			// when no field is explicitly specified in the query.
			Query q = null;
			try {
				q = new QueryParser("name", analyzer).parse(fuzzyQuery);
			} catch (org.apache.lucene.queryparser.classic.ParseException e) {
				e.printStackTrace();
			}

			// 3. search
			int hitsPerPage = 10;
			IndexReader reader = DirectoryReader.open(index);
			IndexSearcher searcher = new IndexSearcher(reader);
			TopScoreDocCollector collector = TopScoreDocCollector.create(hitsPerPage);
			searcher.search(q, collector);
			ScoreDoc[] hits = collector.topDocs().scoreDocs;

			// 4. display results
			System.out.println("Found " + hits.length + " hits.");
			for (int i = 0; i < hits.length; ++i) {
				int docId = hits[i].doc;
				Document d = searcher.doc(docId);
				System.out.println((i + 1) + ". " + d.get("id") + "\t" + d.get("name"));
			}
			// reader can only be closed when there
			// is no need to access the documents any more.
			reader.close();
			
			Utils.println("Query:");
			fuzzyQuery = s.nextLine();
			
			if(fuzzyQuery.endsWith("~") == false)
				fuzzyQuery += "~";
			
			
		}
		
	}

	private static void addDoc(IndexWriter w, String name, String id) throws IOException {
		Document doc = new Document();
		doc.add(new TextField("name", name, Field.Store.YES));

		// use a string field for id because we don't want it tokenized
		doc.add(new StringField("id", id, Field.Store.YES));
		w.addDocument(doc);
	}
	
	private static boolean isValidInput(String[] args)
	{
		if(args.length != 1)
		{
			Utils.println("Please input the data file path as argument (sdn_parsed_data.txt)");
			return false;
		}
		
		return true;
	}
}
