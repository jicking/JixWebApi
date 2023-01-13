using JixWebApp.Core.Entities;
using JixWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace JixWebApp.App.Queries.Tests {
	public class GetAllProjectsQueryHandlerTests {

		private readonly GetAllProjectsQueryHandler _sut;

		public GetAllProjectsQueryHandlerTests() {
			// setup
			// mock dependencies

			//Set EF
			var options = new DbContextOptionsBuilder<JixWebAppDbContext>()
				.UseInMemoryDatabase("JixWebAppDbContext")
				.Options;

			var context = new JixWebAppDbContext(options);

			// seed
			// TODO: Better to seed test scenarios in db init
			var project = new Project() {
				Name = "GetAllTest",
				Description = "GetAllTest"
			};
			context.Projects.Add(project);
			context.SaveChanges();

			_sut = new GetAllProjectsQueryHandler(context);
		}

		[Fact()]
		public async Task HandleTest() {
			var result = await _sut.Handle(new GetAllProjectsQuery(), default);
			Assert.NotEmpty(result);
		}
	}
}