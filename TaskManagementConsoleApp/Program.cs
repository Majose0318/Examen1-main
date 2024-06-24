using TaskManagementLibrary; 

class Program
{
    static bool Confirme(string accion)
    {
        Console.WriteLine("Confirme " + accion + " s/n");
        return Console.ReadLine() == "s";
    } 
     static void Main(string[] args)
    {
        var taskService = new TaskService();

        while (true)
        {
            Console.WriteLine("1. Agregar tarea");
            Console.WriteLine("2. Ver tareas");
            Console.WriteLine("3. Actualizar tarea");
            Console.WriteLine("4. Eliminar tarea");
            Console.WriteLine("5. Completar tarea");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Write("Titulo: ");
                    var title = Console.ReadLine();  
                    if (string.IsNullOrWhiteSpace(title)) title = null;  

                    Console.Write("Descripcion: ");
                    var description = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(description)) description = null;

                    var task = taskService.AddTask(title, description);
                    Console.WriteLine($"Tarea agregada con Id: {task.Id}");
                    break;
                case "2":
                    var tasks = taskService.GetAllTasks();
                    Console.WriteLine("-------------------------------------------------");
                    foreach (var t in tasks)
                    {
                        Console.WriteLine($"ID: {t.Id}, Titulo: {t.Title}, Descripcion: {t.Description}, Completada: {t.IsCompleted}");
                    }
                    Console.WriteLine("-------------------------------------------------");
                    break;
                case "3":
                    Console.Write("Introduzca el Id de la tarea por actualizar: ");
                    var updateId = int.Parse(Console.ReadLine());
                      task = taskService.GetTaskById(updateId);
                  
                    Console.Write($"{task.Title} -> Nuevo titulo: ");
                    var newTitle = Console.ReadLine();
                    
                    Console.Write($"{task.Description} -> Nueva Descripcion: ");
                    var newDescription = Console.ReadLine();
                    Console.Write("Completada (true/false): ");
                    var isCompleted = bool.Parse(Console.ReadLine());
                    
                    
                    if (taskService.UpdateTask(updateId, newTitle, newDescription, isCompleted))
                    {
                        Console.WriteLine("Tarea completada exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("Tarea no encontrada.");
                    }
                    break;
                case "4":
    Console.Write("Introduzca el Id de la tarea a eliminar: ");
    var deleteId = 0;
    try
    {
        deleteId = int.Parse(Console.ReadLine());
    }
    catch (FormatException)
    {
        Console.WriteLine("Error: El Id debe ser un número entero.");
        break;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error inesperado: {ex.Message}");
        break;
    }
                    
                    task = taskService.GetTaskById(deleteId); 
                    
                    if (task == null)
    {
        Console.WriteLine("No se encontró ninguna tarea con el Id especificado.");
        break;
    }
                    Console.WriteLine("Tarea:");
                    Console.Write("     - ");
                    Console.WriteLine(task.Title);
                    if (Confirme("eliminar"))
                    {
                        if (taskService.DeleteTask(deleteId))
                        {
                            Console.WriteLine("Tarea eliminada exitosamente.");
                        }
                        else
                        {
                            Console.WriteLine("Tarea no encontrada.");
                        }
                    }    
                    break;
                case "5":
                    Console.Write("Introduzca el Id de la tarea a completar: ");
                    var completeId = int.Parse(Console.ReadLine());
                    task = taskService.GetTaskById(completeId);

                         if (task == null)
                            {
                                Console.WriteLine("Tarea no encontrada.");
                                break;
                            }

                                Console.WriteLine($"Tarea: {task.Title}");

                         if (taskService.CompleteTask(completeId))
                            {
                                 Console.WriteLine("Tarea completada exitosamente.");
                             }
                         else
                             {
                                Console.WriteLine("No se pudo completar la tarea.");
                            }
                    break;   
                case "6":
                    return;
                default:
                    Console.WriteLine("Opcion invalida, intente de nuevo.");
                    break;
                    
            }
        }
    }
}