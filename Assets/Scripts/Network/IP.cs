using UnityEngine;
using TMPro;
using System.Net;
using System.Net.Sockets;

public class IP : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI statusText;

    [Header("Modbus")]
    public int modbusPort = 502;

    void Start()
    {
        string ip = GetLocalIPAddress();
        statusText.text = $"IP: {ip}";
    }

    string GetLocalIPAddress()
    {
        string localIP = "IP não encontrado";

        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }

        return localIP;
    }
}

