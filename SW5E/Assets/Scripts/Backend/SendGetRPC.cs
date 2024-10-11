using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Postgrest.Responses;

// https://supabase.com/docs/reference/csharp/rpc
public class SendGetRPC : MonoBehaviour
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
#pragma warning restore CS4014
    }

    private async Task SendData(string body, int number)
    {
        // Insert the data (sent as arguments to the function) by calling an RPC function
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
        // Call the RPC function to get data as a JSON string
        BaseResponse result = await supabaseManager.Supabase().Rpc("select_tests", new Dictionary<string, object>());
        Debug.Log(result.Content.ToString());
        resultsText.text = result.Content.ToString();
    }
}
