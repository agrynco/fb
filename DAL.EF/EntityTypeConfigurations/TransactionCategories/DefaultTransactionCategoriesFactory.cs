using Domain.Finances.Transactions;

namespace DAL.EF.EntityTypeConfigurations.TransactionCategories;

public static class DefaultTransactionCategoriesFactory
{
    public static ICollection<TransactionCategory> BuildCategories(int ownerId)
    {
        var originalNodes = Categories.ToList();
        
        int idShift = originalNodes.Max(p => p.Id) + 1;

        List<TransactionCategory> resultNodes = new List<TransactionCategory>();
        for(int i = 0; i < originalNodes.Count; i++)
        {
            var clonedNode = new TransactionCategory
            {
                Id = originalNodes[i].Id + idShift,
                Name = originalNodes[i].Name,
                Description = originalNodes[i].Description,
                OwnerId = ownerId,
                ParentId = originalNodes[i].ParentId == null ? null : originalNodes[i].ParentId + idShift
            };
            
            resultNodes.Add(clonedNode);
        }

        return resultNodes;
    }
    
    public static ICollection<TransactionCategory> Categories
    {
        get
        {
            const int PRODUCTS_CATEGORY_ID = 2;
            const int BILLS_CATEGORY_ID = 14;
            return new List<TransactionCategory>(
                new[]
                {
                    new TransactionCategory
                    {
                        Id = 1,
                        Name = "Прибутки",
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = PRODUCTS_CATEGORY_ID,
                        Name = "Продукти",
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 3,
                        Name = "Молочні",
                        ParentId = PRODUCTS_CATEGORY_ID,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 4,
                        Name = "М'ясні",
                        ParentId = PRODUCTS_CATEGORY_ID,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 5,
                        Name = "Хлібобулочні",
                        ParentId = PRODUCTS_CATEGORY_ID,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 6,
                        Name = "Овочі",
                        ParentId = PRODUCTS_CATEGORY_ID,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 7,
                        Name = "Фрукти",
                        ParentId = PRODUCTS_CATEGORY_ID,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 8,
                        Name = "Ягоди",
                        ParentId = PRODUCTS_CATEGORY_ID,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 9,
                        Name = "Побутова техніка",
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 10,
                        Name = "Особисте здоров'я",
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 11,
                        Name = "Відпочинок",
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 12,
                        Name = "Навчання",
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 13,
                        Name = "Одежа",
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 14,
                        Name = "Комунальні послуги",
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 15,
                        Name = "Опалення",
                        ParentId = BILLS_CATEGORY_ID,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 16,
                        Name = "Гаряча вода",
                        ParentId = BILLS_CATEGORY_ID,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 17,
                        Name = "Холодна вода",
                        ParentId = BILLS_CATEGORY_ID,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 18,
                        Name = "Електроенергія",
                        ParentId = BILLS_CATEGORY_ID,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 19,
                        Name = "Інтернет",
                        ParentId = BILLS_CATEGORY_ID,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 20,
                        Name = "Мобільний зв'язок",
                        ParentId = BILLS_CATEGORY_ID,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 21,
                        Name = "Домашні тварини",
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 22,
                        Name = "Кіт",
                        ParentId = 21,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 23,
                        Name = "Хом'як",
                        ParentId = 21,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 24,
                        Name = "Транспорт",
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 25,
                        Name = "Автомобіль",
                        ParentId = 24,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 26,
                        Name = "Витратні матеріали",
                        ParentId = 25,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 27,
                        Name = "Тормозні колодки",
                        ParentId = 26,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 28,
                        Name = "Передні",
                        ParentId = 27,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 29,
                        Name = "Задні",
                        ParentId = 27,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 30,
                        Name = "Покришки",
                        ParentId = 26,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 31,
                        Name = "Зимові",
                        ParentId = 30,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 32,
                        Name = "Літні",
                        ParentId = 30,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 33,
                        Name = "Рідини",
                        ParentId = 26,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 34,
                        Name = "Омивайка",
                        ParentId = 33,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 35,
                        Name = "Олія моторна",
                        ParentId = 33,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 36,
                        Name = "Олія коробки передач",
                        ParentId = 33,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 37,
                        Name = "Фільтри",
                        ParentId = 26,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 38,
                        Name = "Салон",
                        ParentId = 37,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 39,
                        Name = "Фільтр олії (двигун)",
                        ParentId = 37,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 40,
                        Name = "Повітряний фільтр",
                        ParentId = 37,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 41,
                        Name = "Обслуговування",
                        ParentId = 25,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 42,
                        Name = "Заміна покришок (зима/літо)",
                        ParentId = 41,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 43,
                        Name = "Техогляд (кожні 10 тис. км.)",
                        ParentId = 41,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 44,
                        Name = "Заміна тормозних колодок",
                        ParentId = 43,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 45,
                        Name = "Заміна масла двигуна",
                        ParentId = 43,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 46,
                        Name = "Заміна фільтра олії (двигун)",
                        ParentId = 43,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 47,
                        Name = "Заміна зовнішнього повітряного фільтра",
                        ParentId = 43,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 48,
                        Name = "ГБО",
                        ParentId = 43,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 49,
                        Name = "Паливо",
                        ParentId = 25,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 50,
                        Name = "Газ",
                        ParentId = 49,
                        OwnerId = 1
                    },
                    new TransactionCategory
                    {
                        Id = 51,
                        Name = "Бензин",
                        ParentId = 49,
                        OwnerId = 1
                    }
                }
            );
        }
    }
}