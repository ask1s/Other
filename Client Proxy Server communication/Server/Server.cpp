

#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <Windows.h>

#define SHARED_MEMORY_SIZE 512
#define MEMORY_WRITE_OFFSET 0
#define MEMORY_READ_OFFSET (SHARED_MEMORY_SIZE / 2)

int main()
{
    char name[32];
    printf("Enter shared memory name: ");
    scanf("%s", name);

    HANDLE memoryHandle = CreateFileMappingA(INVALID_HANDLE_VALUE, NULL, PAGE_READWRITE, 0, SHARED_MEMORY_SIZE, name);
    if (memoryHandle == NULL)
    {
        printf("Error allocating shared memory\n");
        return -1;
    }

    char* memory = (char*)MapViewOfFile(memoryHandle, FILE_MAP_ALL_ACCESS, 0, 0, SHARED_MEMORY_SIZE);
    if (memory == NULL)
    {
        CloseHandle(memoryHandle);
        printf("Error mapping memory\n");
        return -1;
    }

    memset(memory, 0, SHARED_MEMORY_SIZE);

    char* memoryWrite = memory + MEMORY_WRITE_OFFSET;
    char* memoryRead = memory + MEMORY_READ_OFFSET;

    char writeSemName[32], readSemName[32];
    sprintf(writeSemName, "%s_sem_write", name);
    sprintf(readSemName, "%s_sem_read", name);

    HANDLE writeSemaphore = CreateSemaphoreA(NULL, 0, 1, writeSemName);
    HANDLE readSemaphore = CreateSemaphoreA(NULL, 0, 1, readSemName);
    if (writeSemaphore == NULL || readSemaphore == NULL)
    {
        UnmapViewOfFile(memory);
        CloseHandle(memoryHandle);
        printf("Cannot create semaphore\n");
        return -1;
    }

    int width, height, width2, height2;

    int error = 4775;
    int ok = 1;

    while (1)
    {

        //Matrix 1
        WaitForSingleObject(readSemaphore, INFINITE);
        if (strlen(memoryRead) == 0)
        {
            break;
        }

        sscanf(memoryRead, "%i", &width);

        sprintf(memoryWrite, "%i", ok);
        ReleaseSemaphore(writeSemaphore, 1, NULL);

        WaitForSingleObject(readSemaphore, INFINITE);
        sscanf(memoryRead, "%i", &height);

        sprintf(memoryWrite, "%i", ok);
        ReleaseSemaphore(writeSemaphore, 1, NULL);

       

        int* matrix = new int[width * height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int info;
                WaitForSingleObject(readSemaphore, INFINITE);
                
                sscanf(memoryRead, "%i", &info);

                sprintf(memoryWrite, "%i", ok);
                ReleaseSemaphore(writeSemaphore, 1, NULL);


                matrix[i + j * width] = info;
            }
        }

        printf("Done creating matrix1\n");
        //Matrix 2
        WaitForSingleObject(readSemaphore, INFINITE);
        
        sprintf(memoryWrite, "%i", ok);
        ReleaseSemaphore(writeSemaphore, 1, NULL);

        sscanf(memoryRead, "%i", &width2);

        WaitForSingleObject(readSemaphore, INFINITE);
        
        sprintf(memoryWrite, "%i", ok);
        ReleaseSemaphore(writeSemaphore, 1, NULL);

        sscanf(memoryRead, "%i", &height2);

        int* matrix2 = new int[width2 * height2];

        for (int i = 0; i < width2; i++)
        {
            for (int j = 0; j < height2; j++)
            {
                int info;
                WaitForSingleObject(readSemaphore, INFINITE);
                
                sprintf(memoryWrite, "%i", ok);
                ReleaseSemaphore(writeSemaphore, 1, NULL);

                sscanf(memoryRead, "%i", &info);

                matrix2[i + j * width2] = info;
            }
        }
        printf("Done creating matrix2\n");

        //Calculating

        int* result_matrix = new int[width * height2];

        if (height != width2)
        {
            sprintf(memoryWrite, "%i", error);
            ReleaseSemaphore(writeSemaphore, 1, NULL);

            WaitForSingleObject(readSemaphore, INFINITE);
        }
        else
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height2; j++)
                {
                    for (int k = 0; k < height; k++)
                    {
                        int first = matrix[i + k * width];
                        int second = matrix2[k + j * width2];

                        int res = first * second;

                        result_matrix[i + j * width] = res;
                    }
                }
            }
        }
        printf("Done calculating\n");

        //Sending answer

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height2; j++)
            {
                int res = result_matrix[i + j * width];

                sprintf(memoryWrite, "%i", res);
                ReleaseSemaphore(writeSemaphore, 1, NULL);

                WaitForSingleObject(readSemaphore, INFINITE);
            }
        }
        printf("Sended answer\n");

        delete[] matrix;
        delete[] matrix2;
        delete[] result_matrix;
        break;
    }

    CloseHandle(writeSemaphore);
    CloseHandle(readSemaphore);
    UnmapViewOfFile(memory);
    CloseHandle(memoryHandle);

    return 0;
}
