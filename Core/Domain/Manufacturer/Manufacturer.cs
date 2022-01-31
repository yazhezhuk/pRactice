using Core.Domains;

namespace Core.Domain.Manufacturer;

public class Manufacturer : BaseEntity<int>
{
	public string Name { get; set; }

}
