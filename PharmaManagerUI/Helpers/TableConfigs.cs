using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PharmaManagerUI.Data;
using PharmaManagerUI.Models;

namespace PharmaManagerUI.Helpers
{
    public static class TableConfigs
    {
        public static List<TableConfig> GetAll()
        {
            try
            {
                var configs = new List<TableConfig>
                {
                    new TableConfig
                    {
                        TableName = "Клиенты",
                        DataSelector = ctx => ctx.Clients.AsQueryable().Cast<object>(),
                        ColumnMappings = new Dictionary<string, Func<object, object>>
                        {
                            { "Название", row => ((Client)row).Name },
                            { "Адрес", row => ((Client)row).Address },
                            { "Контактная информация", row => ((Client)row).ContactInfo }
                        }
                    },
                    new TableConfig
                    {
                        TableName = "Препараты",
                        DataSelector = ctx => ctx.Drugs.AsQueryable().Cast<object>(),
                        ColumnMappings = new Dictionary<string, Func<object, object>>
                        {
                            { "Название", row => ((Drug)row).Name },
                            { "Состав", row => ((Drug)row).Composition },
                            { "Технология производства", row => ((Drug)row).ProductionTechnology },
                            { "Тип упаковки", row => ((Drug)row).PackagingType },
                            { "Информация об маркировке", row => ((Drug)row).LabelingInfo },
                            { "Условия хранения", row => ((Drug)row).StorageConditions }
                        }
                    },
                    new TableConfig
                    {
                        TableName = "Сырьё",
                        DataSelector = ctx => ctx.RawMaterials.AsQueryable().Cast<object>(),
                        ColumnMappings = new Dictionary<string, Func<object, object>>
                        {
                            { "Название", row => ((RawMaterial)row).Name },
                            { "Поставщик", row => ((RawMaterial)row).Supplier },
                            { "Количество на складе", row => ((RawMaterial)row).QuantityInStock },
                            { "Единица измерения", row => ((RawMaterial)row).Unit }
                        }
                    },
                    new TableConfig
                    {
                        TableName = "Оборудование",
                        DataSelector = ctx => ctx.Equipment.AsQueryable().Cast<object>(),
                        ColumnMappings = new Dictionary<string, Func<object, object>>
                        {
                            { "Название", row => ((Equipment)row).Name },
                            { "Тип", row => ((Equipment)row).Type },
                            { "Состояние", row => ((Equipment)row).Status }
                        }
                    },
                    new TableConfig
                    {
                        TableName = "Производственные заказы",
                        DataSelector = ctx => ctx.ProductionOrders.Include(po => po.Client).AsQueryable().Cast<object>(),
                        ColumnMappings = new Dictionary<string, Func<object, object>>
                        {
                            { "Клиент", row => ((ProductionOrder)row).Client?.Name ?? "Не указан" },
                            { "Дата заказа", row => ((ProductionOrder)row).OrderDate?.ToString("d") ?? "Не указана" },
                            { "Дата доставки", row => ((ProductionOrder)row).DeliveryDate?.ToString("d") ?? "Не указана" },
                            { "Статус", row => ((ProductionOrder)row).Status }
                        }
                    },
                    new TableConfig
                    {
                        TableName = "Сотрудники",
                        DataSelector = ctx => ctx.Staff.AsQueryable().Cast<object>(),
                        ColumnMappings = new Dictionary<string, Func<object, object>>
                        {
                            { "Имя", row => ((Staff)row).Name },
                            { "Должность", row => ((Staff)row).Position },
                            { "Отдел", row => ((Staff)row).Department }
                        }
                    },
                    new TableConfig
                    {
                        TableName = "Контроль качества",
                        DataSelector = ctx => ctx.QualityControls.Include(qc => qc.Drug).AsQueryable().Cast<object>(),
                        ColumnMappings = new Dictionary<string, Func<object, object>>
                        {
                            { "Препарат", row => ((QualityControl)row).Drug?.Name ?? "Не указан" },
                            { "Дата теста", row => ((QualityControl)row).TestDate.ToString("d") },
                            { "Тип теста", row => ((QualityControl)row).TestType },
                            { "Результат", row => ((QualityControl)row).Result },
                            { "Комментарии", row => ((QualityControl)row).Comments }
                        }
                    },
                    new TableConfig
                    {
                        TableName = "Склады",
                        DataSelector = ctx => ctx.Warehouses.AsQueryable().Cast<object>(),
                        ColumnMappings = new Dictionary<string, Func<object, object>>
                        {
                            { "Местоположение", row => ((Warehouse)row).Location },
                            { "Вместимость", row => ((Warehouse)row).Capacity }
                        }
                    },
                    new TableConfig
                    {
                        TableName = "Логистика",
                        DataSelector = ctx => ctx.Logistics.Include(l => l.Order).Include(l => l.Warehouse).AsQueryable().Cast<object>(),
                        ColumnMappings = new Dictionary<string, Func<object, object>>
                        {
                            { "Заказ", row => ((Logistic)row).Order?.Id.ToString() ?? "Не указан" },
                            { "Склад", row => ((Logistic)row).Warehouse?.Location ?? "Не указан" },
                            { "Дата доставки", row => ((Logistic)row).DeliveryDate?.ToString("d") ?? "Не указана" },
                            { "Тип транспорта", row => ((Logistic)row).TransportType },
                            { "Статус", row => ((Logistic)row).Status }
                        }
                    },
                    new TableConfig
                    {
                        TableName = "Аптечные сети",
                        DataSelector = ctx => ctx.PharmacyNetworks.AsQueryable().Cast<object>(),
                        ColumnMappings = new Dictionary<string, Func<object, object>>
                        {
                            { "Название", row => ((PharmacyNetwork)row).Name },
                            { "Адрес", row => ((PharmacyNetwork)row).Address },
                            { "Контактная информация", row => ((PharmacyNetwork)row).ContactInfo }
                        }
                    },
                    new TableConfig
                    {
                        TableName = "Продажи",
                        DataSelector = ctx => ctx.Sales.Include(s => s.Pharmacy).Include(s => s.Drug).AsQueryable().Cast<object>(),
                        ColumnMappings = new Dictionary<string, Func<object, object>>
                        {
                            { "Аптечная сеть", row => ((Sale)row).Pharmacy?.Name ?? "Не указана" },
                            { "Препарат", row => ((Sale)row).Drug?.Name ?? "Не указан" },
                            { "Количество", row => ((Sale)row).Quantity },
                            { "Дата продажи", row => ((Sale)row).SaleDate.ToString("d") },
                            { "Общая стоимость", row => ((Sale)row).TotalPrice }
                        }
                    }
                };
                System.Diagnostics.Debug.WriteLine($"TableConfigs loaded {configs.Count} configurations");
                return configs;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in TableConfigs.GetAll: {ex.Message}");
                return new List<TableConfig>();
            }
        }
    }

    public class TableConfig
    {
        public string TableName { get; set; }
        public Func<AppDbContext, IQueryable<object>> DataSelector { get; set; }
        public Dictionary<string, Func<object, object>> ColumnMappings { get; set; }
    }
}