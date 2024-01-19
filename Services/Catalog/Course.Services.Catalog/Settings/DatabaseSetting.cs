﻿namespace Course.Services.Catalog.Settings;

public class DatabaseSetting
{
    public string CourseCollectionName { get; set; }
    public string CategoryCollectionName { get; set; }
    public string PurchasedCoursesOfUserCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    
}