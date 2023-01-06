#define _CRT_SECURE_NO_WARNINGS
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#pragma comment(lib,"Ws2_32.lib")

#define _CRT_SECURE_NO_WARNINGS

#define SHARED_MEMORY_SIZE 512
#define MEMORY_WRITE_OFFSET 0
#define MEMORY_READ_OFFSET (SHARED_MEMORY_SIZE / 2)

#include <stdio.h>
#include <WinSock2.h>
#include <memory.h>
#include <string>
#include <Windows.h>

using namespace std;

SOCKET createListenSocket(int port)
{
    WSADATA wsaData;
    WSAStartup(MAKEWORD(2, 2), &wsaData);

    SOCKET listenSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

    if (listenSocket == INVALID_SOCKET)
    {
        return listenSocket;
    }

    sockaddr_in socketAddr;
    socketAddr.sin_family = AF_INET;
    socketAddr.sin_addr.s_addr = inet_addr("127.0.0.1");
    socketAddr.sin_port = htons(port);

    if (bind(listenSocket, (sockaddr*)&socketAddr, sizeof(socketAddr)) == SOCKET_ERROR)
    {
        printf("Error binding socket\n");
        closesocket(listenSocket);
        WSACleanup();
        return INVALID_SOCKET;
    }
    if (listen(listenSocket, SOMAXCONN) == SOCKET_ERROR)
    {
        printf("Error listening on socket\n");
        closesocket(listenSocket);
        WSACleanup();
        return INVALID_SOCKET;
    }
    return listenSocket;

}

SOCKET acceptClient(SOCKET listenSocket)
{
    SOCKET clientSocket = accept(listenSocket, NULL, NULL);
    if (clientSocket == INVALID_SOCKET)
    {
        printf("Error accepting client\n");
        return INVALID_SOCKET;
    }
    return clientSocket;
}

int main()
{
    //TCP
    int port;
    printf("Enter TCP port: ");
    scanf("%d", &port);

    SOCKET listenSocket = createListenSocket(port);
    if (listenSocket == INVALID_SOCKET)
    {
        return -1;
    }

    printf("TCP socket created\n");
    SOCKET clientSocket = acceptClient(listenSocket);
    if (clientSocket == INVALID_SOCKET)
    {
        closesocket(listenSocket);
        WSACleanup();
        return -1;
    }

    printf("Client connected\n");
    closesocket(listenSocket);

    char name[32];
    printf("Enter shared memory name: ");
    scanf("%s", name);

    // SHMEM
    HANDLE memoryHandle = OpenFileMappingA(FILE_MAP_ALL_ACCESS, FALSE, name);
    if (memoryHandle == NULL)
    {
        printf("Error opening shared memory\n");
        return -1;
    }

    char* memory = (char*)MapViewOfFile(memoryHandle, FILE_MAP_ALL_ACCESS, 0, 0, SHARED_MEMORY_SIZE);
    if (memory == NULL)
    {
        CloseHandle(memoryHandle);
        printf("Error mapping memory\n");
        return -1;
    }

    char* memoryWrite = memory + MEMORY_WRITE_OFFSET;
    char* memoryRead = memory + MEMORY_READ_OFFSET;

    char writeSemName[32], readSemName[32];
    sprintf(writeSemName, "%s_sem_write", name);
    sprintf(readSemName, "%s_sem_read", name);

    HANDLE writeSemaphore = OpenSemaphoreA(SEMAPHORE_MODIFY_STATE | SYNCHRONIZE, FALSE, writeSemName);
    HANDLE readSemaphore = OpenSemaphoreA(SEMAPHORE_MODIFY_STATE | SYNCHRONIZE, FALSE, readSemName);

    if (writeSemaphore == NULL || readSemaphore == NULL)
    {
        UnmapViewOfFile(memory);
        CloseHandle(memoryHandle);
        printf("Cannot open semaphore\n");
        return -1;
    }

    int result;
    char sendBuff[1024], recvBuff[1024];
    
    int width, height, width2, height2;
    int error = 4775;

    do
    {
        /// TCP
        /// Matrix 1
        
        memset(recvBuff, 0, 1024);
        result = recv(clientSocket, recvBuff, 1024, 0);
        if (result == SOCKET_ERROR)
        {
            printf("Receive failed\n");
            closesocket(clientSocket);
            WSACleanup();
            return -1;
        }
        sscanf(recvBuff, "%i", &width);

        memset(recvBuff, 0, 1024);
        result = recv(clientSocket, recvBuff, 1024, 0);
        if (result == SOCKET_ERROR)
        {
            printf("Receive failed\n");
            closesocket(clientSocket);
            WSACleanup();
            return -1;
        }
        sscanf(recvBuff, "%i", &height);

        int* matrix = new int[width * height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int cislo;
                memset(recvBuff, 0, 1024);
                result = recv(clientSocket, recvBuff, 1024, 0);
                if (result == SOCKET_ERROR)
                {
                    printf("Receive failed\n");
                    closesocket(clientSocket);
                    WSACleanup();
                    return -1;
                }
                sscanf(recvBuff, "%i", &cislo);
                matrix[i + j * width] = cislo;
            }
        }
        printf("Done creating matrix1\n");
        /// Matrix 2
        memset(recvBuff, 0, 1024);
        result = recv(clientSocket, recvBuff, 1024, 0);
        if (result == SOCKET_ERROR)
        {
            printf("Receive failed\n");
            closesocket(clientSocket);
            WSACleanup();
            return -1;
        }
        sscanf(recvBuff, "%i", &width2);

        memset(recvBuff, 0, 1024);
        result = recv(clientSocket, recvBuff, 1024, 0);
        if (result == SOCKET_ERROR)
        {
            printf("Receive failed\n");
            closesocket(clientSocket);
            WSACleanup();
            return -1;
        }
        sscanf(recvBuff, "%i", &height2);

        int* matrix2 = new int[width2 * height2];

        for (int i = 0; i < width2; i++)
        {
            for (int j = 0; j < height2; j++)
            {
                int cislo;
                memset(recvBuff, 0, 1024);
                result = recv(clientSocket, recvBuff, 1024, 0);
                if (result == SOCKET_ERROR)
                {
                    printf("Receive failed\n");
                    closesocket(clientSocket);
                    WSACleanup();
                    return -1;
                }
                sscanf(recvBuff, "%i", &cislo);
                matrix2[i + j * width2] = cislo;
            }
        }
        printf("Done creating matrix2\n");

        //SHMEM
        //Send Matrix 1
        sprintf(memoryRead, "%i", width);
        ReleaseSemaphore(readSemaphore, 1, NULL);

        WaitForSingleObject(writeSemaphore, INFINITE);


        sprintf(memoryRead, "%i", height);
        ReleaseSemaphore(readSemaphore, 1, NULL);

        WaitForSingleObject(writeSemaphore, INFINITE);
        

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                sprintf(memoryRead, "%i", matrix[i + j * width]);
                ReleaseSemaphore(readSemaphore, 1, NULL);

                WaitForSingleObject(writeSemaphore, INFINITE);
            }
        }
        printf("Sended matrix1\n");

        //Send Matrix 2
        sprintf(memoryRead, "%i", width2);
        ReleaseSemaphore(readSemaphore, 1, NULL);

        WaitForSingleObject(writeSemaphore, INFINITE);

        sprintf(memoryRead, "%i", height2);
        ReleaseSemaphore(readSemaphore, 1, NULL);

        WaitForSingleObject(writeSemaphore, INFINITE);


        for (int i = 0; i < width2; i++)
        {
            for (int j = 0; j < height2; j++)
            {
                sprintf(memoryRead, "%i", matrix2[i + j * width2]);
                ReleaseSemaphore(readSemaphore, 1, NULL);

                WaitForSingleObject(writeSemaphore, INFINITE);

            }
        }
        printf("Sended matrix2\n");

        delete[] matrix;
        delete[] matrix2;
        int* result_matrix = new int[width * height2];

        //Answer
        int answer;
        int ok = 1;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height2; j++)
            {
                WaitForSingleObject(writeSemaphore, INFINITE);

                sprintf(memoryRead, "%i", ok);
                ReleaseSemaphore(readSemaphore, 1, NULL);

                sscanf(memoryWrite, "%i", &answer);

                if (answer == error)
                {
                    printf("Error\n");
                    sprintf(sendBuff, "%i", error);
                    if (send(clientSocket, sendBuff, strlen(sendBuff) + 1, 0) == SOCKET_ERROR)
                    {
                        printf("Send failed\n");
                        closesocket(clientSocket);
                        WSACleanup();
                        return -1;
                    }
                    break;
                }

                result_matrix[i + j * width] = answer;
            }
        }
        printf("Received answer\n");

        //Sending answer

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height2; j++)
            {
                int res = result_matrix[i + j * width];

                sprintf(sendBuff, "%i", res);
                if (send(clientSocket, sendBuff, strlen(sendBuff) + 1, 0) == SOCKET_ERROR)
                {
                    printf("Send failed\n");
                    closesocket(clientSocket);
                    WSACleanup();
                    return -1;
                }

            }
        }
        printf("Sended answer\n");
    
        delete[] result_matrix;
        break;

    } while (result > 0);

    closesocket(clientSocket);
    WSACleanup();

    sprintf(memoryRead, "");
    ReleaseSemaphore(readSemaphore, 1, NULL);
    CloseHandle(writeSemaphore);
    CloseHandle(readSemaphore);
    UnmapViewOfFile(memory);
    CloseHandle(memoryHandle);

    return 0;
}