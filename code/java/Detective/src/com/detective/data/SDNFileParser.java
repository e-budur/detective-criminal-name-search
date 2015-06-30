package com.detective.data;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;

import com.detective.datamodel.SDN;

public class SDNFileParser {

	private String filename;
	
	public SDNFileParser(String file)
	{
		this.filename = file;
	}
	
	public ArrayList<SDN> readAllSDNs()
	{
		try {
			List<String> lines = Files.readAllLines(Paths.get(filename));
			
			ArrayList<SDN> sdnList = parseSDNs(lines);
			
			return sdnList;
			
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		ArrayList<SDN> emptySDNList = new ArrayList<SDN>();
		
		return emptySDNList;
	}
	
	private ArrayList<SDN> parseSDNs(List<String> lines)
	{
		ArrayList<SDN> sdnList = new ArrayList<SDN>();
		
		for(String line:lines)
		{
			SDN sdn = new SDN(line);
			sdnList.add(sdn);
		}
		return sdnList;
	}
}
