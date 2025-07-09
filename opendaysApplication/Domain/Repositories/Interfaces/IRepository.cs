using System.Linq.Expressions;

namespace Domain.Repositories.Interfaces;

// Eine Schnittstelle definiert einen Contract zwischen Schnittstelle selbst
// und der implementierenden Klasse

// Eine Schnittstelle besteht aus einer freien Abfolge von Methodenköpfen
public interface IRepository<TEntity> where TEntity:class
{
    // Rückgabewert TEntity- Datenbank ist für die Generierung der ID verantwortlich
    TEntity Create(TEntity t);

    List<TEntity> CreateRange(List<TEntity> list);

    void Update(TEntity t);

    void UpdateRange(List<TEntity> list);
    
    //Als Parameter wird eine ID Uebergeben. Der Rueckgabewert repr'esentiert
    //die entsprechende Entitaet, falls sie vorhanden ist
    TEntity? Read(int id);
    
    TEntity? Read(string id);

    // Die Methode erwartet als Parameter eine Filterfunktion, Der Filter
    // wird generisch auf die Daten der Abfrage angewandt und aknn je nach
    // Beschaffenheit unterschiedeliche Werte an den Aufrufen zureckgeben
    List<TEntity> Read(Expression<Func<TEntity, bool>> filter);

    //Alle Eintraege aus einer bestimmten Tabelle strukturiert in seiten 
    List<TEntity> Read(int start, int count);

    // select * from TEntity;
    List<TEntity> ReadAll();

    void Delete(TEntity t);

}
