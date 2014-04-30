using SQLite;

namespace TonkostiLocationParser.Db
{
	[Table("countries")]
	public class CountryDataObject
	{
		[Column("id")]
		[PrimaryKeyAttribute]
		[AutoIncrementAttribute]
		public int id { get; set; }

		[Column("name")]
		public string name { get; set; }

		[Column("is_important")]
		public int is_important { get; set; }
	}
}
