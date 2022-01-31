using System.Linq.Expressions;
using Core;
using Core.Domain.Manufacturer;
using Core.Domains;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppContext : DbContext
{
	public DbSet<Receipt> Receipts { get; set; } = null!;
	public DbSet<Record> Records { get; set; } = null!;
	public DbSet<Manufacturer> Manufacturers { get; set; } = null!;

	public AppContext(DbContextOptions<AppContext> options,
	IMediator mediator):base(options)
	{
		_mediator = mediator;

	}

	private readonly IMediator _mediator;

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
	{
		int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

		// ignore events if no dispatcher provided

		var entitiesWithEvents = ChangeTracker
			.Entries()
			.Select(e => e.Entity as BaseEntity<int>)
			.Where(e => e?.Events != null && e.Events.Any())
			.ToArray();

		foreach (var entity in entitiesWithEvents)
		{
			var events = entity.Events.ToArray();
			entity.Events.Clear();
			foreach (var domainEvent in events)
			{
				await _mediator.Publish(domainEvent, cancellationToken).ConfigureAwait(false);
			}
		}
		return result;
	}


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		var something = Receipts.AsQueryable();

		Expression<Func<IQueryable<Receipt>, object>> expr = p => p.Include(d => d.Records);
		var e = expr.Compile()(something);
	}

	public override int SaveChanges()
	{
		return SaveChangesAsync().GetAwaiter().GetResult();
	}



}
