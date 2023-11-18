//Задача 3 - Журнал + статьи
//1.Ввод информации о журнале
//2. Вывод информации о журнале
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;


List<Article> list = new List<Article>(3);
list.Add(new Article("Surjery", "scientic article", 5));
list.Add(new Article("Flue and illness", "researching of flue cure", 10));
list.Add(new Article("Stomac ache", "Benefits of fruits in the diet", 12));


Journal TheLancet = new Journal("The Lancet", "ElSeiver publishing", 1823, 120, list);
Console.WriteLine(TheLancet);

//3. Сериализация журнала
//4.Сохранение сериализованного журнала в файл
using (FileStream file = new FileStream("journal.json", FileMode.OpenOrCreate))
{
    DataContractJsonSerializer journalSaver = new DataContractJsonSerializer(typeof(Journal));
    journalSaver.WriteObject(file, TheLancet);
}

//5.Загрузка сериализованного журнала из файла. После загрузки нужно произвести десериализацию журнала.
Journal? LoadedJournal;
using (FileStream file = new FileStream("journal.json", FileMode.Open))
{
    DataContractJsonSerializer journalSaver = new DataContractJsonSerializer(typeof(Journal));
    LoadedJournal = (Journal?)journalSaver.ReadObject(file);
    Console.WriteLine("\nDeserialized journal");
    Console.WriteLine(LoadedJournal);
}


[Serializable]
[DataContract]
public class Journal
{
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public string Publisher { get; set; }

    [DataMember]
    public uint Date { get; set; }
    [DataMember]
    public uint Pages { get; set; }
    [DataMember]
    public List<Article> ArticleList { get; set; }

    public Journal(string name, string publisher, uint date, uint pages, List<Article> article)
    {
        Name = name;
        Publisher = publisher;
        Date = date;
        Pages = pages;
        ArticleList = article;
    }
    public override string ToString()
    {
        StringBuilder temp = new StringBuilder(255);
        temp.Append($"\"{Name ?? "NoData"}\".{Publisher ?? "NoData"} - {Date} - {Pages}p\n");
        temp.Append("Articles of the Journal:");
        foreach (Article a in ArticleList)
            temp.Append(a.ToString() + '\n');
        return temp.ToString() + '\n';
    }
 }

public class Article
{
    public string Title { get; set; }
    public string Description { get; set; }
    public uint Pages { get; set; }

    public Article()
    {
        Title = "";
        Description = "";
        Pages = 0;
    }
    public Article(string title, string description, uint pages)
    {
        Title = title;
        Description = description;
        Pages = pages;
    }
    public override string ToString()
    {
        return $"\"{Title}\" ({Description}), {Pages}p.";
    }
}