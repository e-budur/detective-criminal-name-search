package com.detective.datamodel;

import java.util.ArrayList;
import java.util.List;

public class SDN {

	private int id;
	private String firstname;
	private String surname;
	private String[] aliases;
	
	private final int MIN_FIELD_COUNT = 3;
	public SDN(String line) {
		// TODO Auto-generated constructor stub
		parseFields(line);
	}

	private void parseFields(String line) {
		// TODO Auto-generated method stub
		String[] fields = line.split("\\|");
		if(fields.length < MIN_FIELD_COUNT)
			return;
		
		this.id = Integer.parseInt(fields[0]);
		this.firstname = fields[1].trim();
		this.surname = fields[2].trim();
		if(fields.length >= 4)
			this.aliases = fields[3].split(";");
	}
	
	public List<String> getNames()
	{
		List<String> names = new ArrayList<String>();
		names.add((this.firstname + " "+ this.surname).trim());
		if(aliases != null)
			for(String alias : aliases)
				names.add(alias);
		
		return names;
	}
	
	public int getId()
	{
		return id;
	}
	
	public String toString()
	{
		StringBuilder sb = new StringBuilder();
		sb.append("ID:"+this.id);
		
		sb.append("  name:"+this.firstname+" "+this.surname+ "\n");
		
		if(aliases != null)
			for(String alias: this.aliases)
				sb.append("  name:"+alias+ "\n");
		
		return sb.toString();
	}
}
