
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext {

    protected override void OnConfiguring(DbContextOptionsBuilder builder){
        string con = "server=localhost;port=3306;"+"database=listacontatos;user=root;password=admin";
        builder.UseMySQL(con);
    }
    
    public DbSet<Contato> Contatos => Set<Contato>();

}




public class Banco
{
    private static List<Contato> contatos = new List<Contato>
    {
        new Contato { Id = 1, Nome = "Vitor", Email = "teste@hotmail.com", NumeroTelefone = "42991447852" },
        new Contato { Id = 2, Nome = "Tone", Email = "teste@gmail.com", NumeroTelefone = "42991347852" }
    };

    public static List<Contato> getContatos()
    {
        return contatos;
    }

    public static Contato getContato(int id)
    {
        return contatos.FirstOrDefault(t => t.Id == id);
    }

    public static Contato addContato(Contato contato)
    {
        contato.Id = contatos.Count + 1;
        contatos.Add(contato);
        return contato;
    }

    public static Contato updateContato(int id, Contato contato)
    {
        var contatoExistente = contatos.FirstOrDefault(t => t.Id == id);
        if (contatoExistente == null)
        {
            return null;
        }

        contatoExistente.Nome = contato.Nome;
        contatoExistente.Email = contato.Email;
        contatoExistente.NumeroTelefone = contato.NumeroTelefone;
        
        return contatoExistente;
    }

    public static bool deleteContato(int id)
    {
        var contatoExistente = contatos.FirstOrDefault(t => t.Id == id);
        if (contatoExistente == null)
        {
            return false;
        }

        contatos.Remove(contatoExistente);
        return true;
    }

}