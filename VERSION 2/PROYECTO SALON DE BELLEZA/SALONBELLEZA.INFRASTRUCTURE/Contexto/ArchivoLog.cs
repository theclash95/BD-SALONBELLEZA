namespace SALONBELLEZA.INFRASTRUCTURE.Contexto;
public static class ArchivoLog
{
    public static void RegistrarError(Exception ex)
    {
        try
        {
            var dir=Path.Combine(AppContext.BaseDirectory,"Errorlog");
            Directory.CreateDirectory(dir);
            var file=Path.Combine(dir,$"{DateTime.Now:yyyy-MM-dd}.txt");
            File.AppendAllText(file,$"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {ex}\n");
        } catch { }
    }
}