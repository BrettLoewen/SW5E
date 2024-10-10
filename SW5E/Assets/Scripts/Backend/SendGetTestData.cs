using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Supabase.Realtime.PostgresChanges;
using Postgrest.Models;
using Supabase.Realtime;
using System.Linq.Expressions;
using Supabase.Interfaces;
using Postgrest.Responses;
using Supabase;
using Supabase.Gotrue;
using Client = Supabase.Client;
using Unity.VisualScripting;

public class Tests : BaseModel
{
    public int id;
    public string createdAt;
    public string body;
    public int number;

    //public Tests(string body, int number)
    //{
    //    this.body = body;
    //    this.number = number;
    //}
}

public class SendGetTestData : MonoBehaviour
{
    public TMP_InputField bodyInput = null;
    public TMP_InputField numberInput = null;
    public TMP_Text errorText = null;
    public TMP_Text resultsText = null;
    public SupabaseManager supabaseManager = null;

    public void SendData()
    {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        SendData(bodyInput.text, int.Parse(numberInput.text));
        //SendData(int.Parse(numberInput.text), bodyInput.text);
#pragma warning restore CS4014
    }

    private async Task SendData(string body, int number)
    {
        //await supabaseManager.Supabase().From<Tests>().On(PostgresChangesOptions.ListenType.All, (sender, response) =>
        //{
        //    switch (response.Event)
        //    {
        //        case Constants.EventType.Insert:
        //            break;
        //        case Constants.EventType.Update:
        //            break;
        //        case Constants.EventType.Delete:
        //            break;
        //    }

        //    Debug.Log($"[{response.Event}]:{response.Topic}:{response.Payload.Data}");
        //});

        ////Tests testData = new(body, number);
        //Tests testData = new();
        //testData.body = body;
        //testData.number = number;

        //await supabaseManager.Supabase().Postgrest.Table<Tests>().Insert(testData);

        await supabaseManager.Supabase().Rpc("insert_tests", new Dictionary<string, object> { { "p_body", body }, { "p_number", number } });
        Debug.Log("Data inserted");
    }

    public void GetData()
    {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        FetchData();
#pragma warning restore CS4014
    }

    private async Task FetchData()
    {
        //await supabaseManager.Supabase().Postgrest.Table<Tests>().Select();
        //await supabaseManager.Supabase().Postgrest.Table<Tests>().Select(;

        // Call the RPC function to get data as JSON
        BaseResponse result = await supabaseManager.Supabase().Rpc("select_tests", new Dictionary<string, object>());
        Debug.Log(result.Content.ToString());
        resultsText.text = result.Content.ToString();
    }

    private async Task SendData(int number, string body)
    {
        string supabaseUrl = "https://vniqxahnjrzkidcexkky.supabase.co";
        string supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZuaXF4YWhuanJ6a2lkY2V4a2t5Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjgzNTEzMDcsImV4cCI6MjA0MzkyNzMwN30.hAKSJ--2gkVNnLOKUOhK5UrAeXWb5h-IsQuIBhjbAXs";

        Client supabaseClient;

        // Initialize Supabase client
        supabaseClient = new Client(supabaseUrl, supabaseKey);
        await supabaseClient.InitializeAsync();

        // Log in the user
        var user = await supabaseClient.Auth.SignIn("brettzky+test@example.com", "test200!");
        Debug.Log("User authenticated: " + user.User.Email);

        // Now call the RPC function as the authenticated user
        var result = await supabaseClient.Rpc("insert_test", new Dictionary<string, object> { { "p_body", body }, { "p_number", number } });
        Debug.Log("Data inserted");
    }
}
