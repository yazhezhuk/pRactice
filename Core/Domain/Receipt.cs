namespace Core.Domains;

public class Receipt
{
	public List<Record> Records { get; }
	public double GoldPrice { get; set; }

	public void AddRecord(Record record)
	{
		Records.Add(record);
	}

	public void ReEvaluatePrice()
	{
		foreach (var record in Records)
		{
			record.Jewelery.Price = GoldPrice * record.Jewelery.Weight;
		}
	}
}
