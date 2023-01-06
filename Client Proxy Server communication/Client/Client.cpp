#define _CRT_SECURE_NO_WARNINGS
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#pragma comment(lib,"Ws2_32.lib")

#include <chrono>
#include <ctime>   
#include <stdio.h>
#include <WinSock2.h>
#include <memory.h>
#include <string>
#include <fstream>

using namespace std;

SOCKET createClientSocket(const char* address, int port)
{
    WSADATA wsaData;
    WSAStartup(MAKEWORD(2, 2), &wsaData);

    SOCKET clientSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

    if (clientSocket == INVALID_SOCKET)
    {
        return clientSocket;
    }

    sockaddr_in socketAddr;
    socketAddr.sin_family = AF_INET;
    socketAddr.sin_addr.s_addr = inet_addr(address);
    socketAddr.sin_port = htons(port);

    if (connect(clientSocket, (sockaddr*)&socketAddr, sizeof(socketAddr)) == SOCKET_ERROR)
    {
        printf("Error connecting to server\n");
        closesocket(clientSocket);
        WSACleanup();
        return INVALID_SOCKET;
    }
    return clientSocket;
}


int main()
{
    char address[16];
    printf("Enter server address: ");
    scanf("%s", address);
    int port;
    printf("Enter TCP port: ");
    scanf("%d", &port);


    SOCKET clientSocket = createClientSocket(address, port);
    if (clientSocket == INVALID_SOCKET)
    {
        return -1;
    }
    printf("Connected to server\n");

    char sendBuff[1024], recvBuff[1024];
    int width, height;
    int width2, height2;
    int error = 4775;

    //Matrix1
    do 
    {
        printf("Enter the matrix sizes(width,height): "); 
        scanf("%i", &width);
        scanf("%i", &height);

        if (width <= 0 || height <= 0)
            printf("Incorrect sizes\n");

    } while (width <= 0 && height <= 0);

    int* matrix = new int[width * height];
    for (int i = 0; i < width; i++)
    {
        for (int j = 0; j < height; j++)
        {
            int a;
            do
            {
                printf("Write next number for matrix: ");
                scanf("%i", &a);
                matrix[i + j * width] = a;
                if (a > 100 || a < -100)
                {
                    printf("Incorrect number(Max: 99, Min: -99\n");
                }

            } while (a > 100 || a < -100);
        }
    }
    printf("Done creating matrix1\n");

    //Matrix2
    do
    {
        printf("Enter the second matrix sizes(width,height): ");
        scanf("%i", &width2);
        scanf("%i", &height2);

        if (width2 <= 0 || height2 <= 0)
            printf("Incorrect sizes\n");

    } while (width2 <= 0 && height2 <= 0);

    int* matrix2 = new int[width2 * height2];
    for (int i = 0; i < width2; i++)
    {
        for (int j = 0; j < height2; j++)
        {
            int a;
            do
            {
                printf("Write next number for matrix: ");
                scanf("%i", &a);
                matrix2[i + j * width2] = a;
                if (a > 100 || a < -100)
                {
                    printf("Incorrect number(Max: 99, Min: -99\n");
                }

            } while (a > 100 || a < -100);
        }
    }
    printf("Done creating matrix2\n");

    while (true)
    {
        //Send matrix 1
        sprintf(sendBuff, "%i", width);
        if (send(clientSocket, sendBuff, strlen(sendBuff) + 1, 0) == SOCKET_ERROR)
        {
            printf("Send failed\n");
            closesocket(clientSocket);
            WSACleanup();
            return -1;
        }

        sprintf(sendBuff, "%i", height);
        if (send(clientSocket, sendBuff, strlen(sendBuff) + 1, 0) == SOCKET_ERROR)
        {
            printf("Send failed\n");
            closesocket(clientSocket);
            WSACleanup();
            return -1;
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                sprintf(sendBuff, "%i", matrix[i + j * width]);
                if (send(clientSocket, sendBuff, strlen(sendBuff) + 1, 0) == SOCKET_ERROR)
                {
                    printf("Send failed\n");
                    closesocket(clientSocket);
                    WSACleanup();
                    return -1;
                }
            }
        }
        printf("Sended matrix1\n");

        //Send matrix 2
        sprintf(sendBuff, "%i", width2);
        if (send(clientSocket, sendBuff, strlen(sendBuff) + 1, 0) == SOCKET_ERROR)
        {
            printf("Send failed\n");
            closesocket(clientSocket);
            WSACleanup();
            return -1;
        }

        sprintf(sendBuff, "%i", height2);
        if (send(clientSocket, sendBuff, strlen(sendBuff) + 1, 0) == SOCKET_ERROR)
        {
            printf("Send failed\n");
            closesocket(clientSocket);
            WSACleanup();
            return -1;
        }

        for (int i = 0; i < width2; i++)
        {
            for (int j = 0; j < height2; j++)
            {
                sprintf(sendBuff, "%i", matrix2[i + j * width2]);
                if (send(clientSocket, sendBuff, strlen(sendBuff) + 1, 0) == SOCKET_ERROR)
                {
                    printf("Send failed\n");
                    closesocket(clientSocket);
                    WSACleanup();
                    return -1;
                }
            }
        }
        printf("Sended matrix2\n");


        delete[] matrix;
        delete[] matrix2;
        int* result_matrix = new int[width * height2];

        //Answer
        int res;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height2; j++)
            {
                int res;
                memset(recvBuff, 0, 1024);
                if (recv(clientSocket, recvBuff, 1024, 0) == SOCKET_ERROR)
                {
                    printf("Error receiving data\n");
                    closesocket(clientSocket);
                    WSACleanup();
                    return -1;
                }
                sscanf(recvBuff, "%i", &res);

                if (res == error)
                {
                    printf("Can't multiply two matrices\n");
                    break;
                }

                result_matrix[i + j * width] = res;
            }
        }
        printf("Received answer\n");

        //Printing answer
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height2; j++)
            {
                printf("%i ", result_matrix[i + j * width]);

                if (j == height2 - 1)
                    printf("\n");
            }
        }

        printf("Printed\n");
 
        delete[] result_matrix;
    }
    closesocket(clientSocket);
    WSACleanup();
    return 0;
}