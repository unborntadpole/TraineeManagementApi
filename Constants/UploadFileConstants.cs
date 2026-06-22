namespace TraineeManagementApi.Constants;

public class UploadFilesConstants
{
    public static List<string> AllowedExtensions = [".txt", ".pdf"];

    public static string UploadDirectory = "UploadedFiles";

    public static string RequestPath = "/UploadedFiles";

    public static long MaxLength =  1 * 1024; // 20_971_520; // this is 20 mb

}