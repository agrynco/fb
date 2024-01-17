using Services.Users.Mailing;

namespace Services.Tests;

using System.Collections.Immutable;
using DAL.EF.EntityTypeConfigurations.TransactionCategories;
using Domain.Finances.Transactions;

public class OutgoingEmailTemplateProcessorTests
{
    [Fact]
    public void Apply()
    {
        const string template = "Some text {{PlaceHolder1}} some text {{PlaceHolder2}} some text";

        object templateModel = new
        {
            PlaceHolder1 = "Value1",
            PlaceHolder2 = "Value2"
        };
        
        string result = OutgoingEmailTemplateProcessor.Apply(template, templateModel);
        
        Assert.Equal("Some text Value1 some text Value2 some text", result);
    }

    [Fact]
    public void CloneTransactionCategories()
    {
        List<TransactionCategory> originalNodes = GetNodesFromDatabase();

        int idShift = originalNodes.Max(p => p.Id) + 1;

        List<TransactionCategory> resultNodes = new List<TransactionCategory>();
        for(int i = 0; i < originalNodes.Count; i++)
        {
            var clonedNode = new TransactionCategory
            {
                Id = originalNodes[i].Id + idShift,
                Name = originalNodes[i].Name,
                Description = originalNodes[i].Description,
                OwnerId = originalNodes[i].OwnerId,
                ParentId = originalNodes[i].ParentId == null ? null : originalNodes[i].ParentId + idShift
            };
            
            resultNodes.Add(clonedNode);
        }
    }
    
    static List<TransactionCategory> GetNodesFromDatabase()
    {
        return new List<TransactionCategory>(DefaultTransactionCategoriesFactory.Categories);
    }
    
}