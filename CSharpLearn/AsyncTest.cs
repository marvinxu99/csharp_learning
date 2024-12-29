// Non-async, task-returning method.
// Within this method (but outside of the local function),
// any thrown exceptions emerge synchronously.
namespace CSharpLearn;

internal class AsyncTest
{
    public class Toast { }

    public static Task<Toast> ToastBreadAsync(int slices, int toastTime)
    {
        if (slices is < 1 or > 4)
        {
            throw new ArgumentException(
                "You must specify between 1 and 4 slices of bread.",
                nameof(slices));
        }

        if (toastTime < 1)
        {
            throw new ArgumentException(
                "Toast time is too short.", nameof(toastTime));
        }

        return ToastBreadAsyncCore(slices, toastTime);

        // Local async function.
        // Within this function, any thrown exceptions are stored in the task.
        static async Task<Toast> ToastBreadAsyncCore(int slices, int time)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            // Start toasting.
            await Task.Delay(time);

            if (time > 2_000)
            {
                throw new InvalidOperationException("The toaster is on fire!");
            }

            Console.WriteLine("Toast is ready!");

            return new Toast();
        }
    }

    public static async Task RunTest()
    {
        try
        {
            // Synchronous validation happens here
            Task<Toast> toastingTask = ToastBreadAsync(2, 2500);

            // Async exceptions will propagate here
            Toast toast = await toastingTask;

            Console.WriteLine("Enjoy your toast!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation error: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Toasting error: {ex.Message}");
        }
    }

}

