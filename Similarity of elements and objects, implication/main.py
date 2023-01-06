import pandas as pd
import matplotlib.pyplot as plt
import numpy as np
from math import isnan

implict_dic = {
                "41": 0.25,
                "42": 0.5
                "43": 0.75,
                "44": 1.0,
                "45": 1.0,
                "51": 0.0,
                "52": 0.25,
                "53": 0.50,
                "54": 0.75,
                "55": 1.0,
}
# print(implict_dic['52'])
data = pd.read_csv('rt.csv')
print(data)
print("-----------------------------------------------------------------------------")
df_dic = data.to_dict()
print(df_dic)
print("==============================================================")

# ggg = list(df_dic.values())
# print(ggg)
# for i in ggg:
#     gg = list(ggg.values())
#     print(gg, "-==--=-=-=--=--==-==-=")


# print(df_dic.items())

# for key in df_dic:
#     print(key)
#     for item in df_dic[key]:
#         print(item)
D = df_dic["D"]
D = list(D.values())
D = [x for x in D if str(x) != 'nan']
print("D = ", D)

N = df_dic["NM"][0]
print("N = ", N)

plane = df_dic["Vj1"]
plane = list(plane.values())
plane.pop(0)
print("lietadlo = ", plane)

bus = df_dic["Vj2"]
bus = list(bus.values())
bus.pop(0)
print("autobus = ", bus)

train = df_dic["Vj3"]
train = list(train.values())
train.pop(0)
print("vlak = ", train)

boat = df_dic["Vj4"]
boat = list(boat.values())
boat.pop(0)
print("lod = ", boat)

n = 4 #count of transport
Pk = 3
Pp = 4
W = 0.99


# keys = df_dic.keys()
# print(keys)
# print(len(keys))
# for key in keys:
#     ttt = df_dic.get(key)
#     print(ttt, "=============")
#     ggg = list(ttt.values())
#     print("GGG = ", ggg)
#     print(key)
#     for item in df_dic[key]:
#         print(df_dic[key][item])





print("-----------------------------------------------------------------------------")

# D = data.D[1:].tolist()
# lietadlo = data.Vj1[1:].tolist()
# autobus = data.Vj2[1:].tolist()
# vlak = data.Vj3[1:].tolist()
# lod = data.Vj4[1:].tolist()
# N = 5 #from csv
# n = 4 #count of transport
# Pk = 3
# Pp = 4
# W = 0.99

capacity = [data.Vj1[1], data.Vj2[1], data.Vj3[1], data.Vj4[1]]
speed = [data.Vj1[2], data.Vj2[2], data.Vj3[2], data.Vj4[2]]
price = [data.Vj1[3], data.Vj2[3], data.Vj3[3], data.Vj4[3]]

# -----------------------------------------------------------------------------
def rules(D, n, N, W, lst):
    result = list()
    lst_r = revers(lst)
    dmax = D[0]
    for i in range(len(D)):
        if(dmax < D[i]):
            dmax = D[i]


    f1 = 1 - (1 - W)**(1 / n)
# ----------------# mk =
    f2l = (int(lst[0]) - (((N + 1) / 2)))/((N - 1) / 2)
    # f2r = f2l * -1
    f3 = D[0]/dmax
    mk = f1 * f2l * f3
    result.append(mk)
# ----------------# vk =
    f2r = f2l * -1
    f3 = D[0]/dmax
    vk = f1 * f2r * f3
    result.append(vk)
# ----------------# mr =
    f2l = (int(lst[1]) - (((N + 1) / 2)))/((N - 1) / 2)
    # f2r = f2l * -1
    f3 = D[1]/dmax
    mr = f1 * f2l * f3
    result.append(mr)
# ----------------# vr =
    f2r = f2l * -1
    f3 = D[1]/dmax
    vr = f1 * f2r * f3
    result.append(vr)
# ----------------# mc =
    f2l = (int(lst[2]) - (((N + 1) / 2)))/((N - 1) / 2)
    # f2r = f2l * -1
    f3 = D[2]/dmax
    mc = f1 * f2l * f3
    result.append(mc)
# ----------------# vc =
    f2r = f2l * -1
    f3 = D[2]/dmax
    vc = f1 * f2r * f3
    result.append(vc)


    return result

# -----------------------------------------------------------------------------

def implication(implict_dic, lst1, lst2):
    result = list()
    lst1_r = revers(lst1)
    lst2_r = revers(lst2)
    temp_lst1 = list()
    temp_lst2 = list()
    temp = list()
# ----------------# m1_m2 =
    for i in range(len(lst1)):
        if(int(lst1[i]) > 3):
            temp_lst1.append(lst1[i])
            temp_lst2.append(lst2[i])
    if (len(temp_lst1) > 1):
        for i in range(len(temp_lst1)):
            name = str(temp_lst1[i]) + str(temp_lst2[i])
            temp.append(implict_dic[name])
        m1_m2 = sum(temp) / len(temp)
    else:
        name = str(temp_lst1[0]) + str(temp_lst2[0])
        m1_m2 = implict_dic[name]
    result.append(m1_m2)
    temp_lst1.clear()
    temp_lst2.clear()
    temp.clear()
# ----------------# m1_v2 =
    for i in range(len(lst1)):
        if(int(lst1[i]) > 3):
            temp_lst1.append(lst1[i])
            temp_lst2.append(lst2_r[i])
    if (len(temp_lst1) > 1):
        for i in range(len(temp_lst1)):
            name = str(temp_lst1[i]) + str(temp_lst2[i])
            temp.append(implict_dic[name])
        m1_v2 = sum(temp) / len(temp)
    else:
        name = str(temp_lst1[0]) + str(temp_lst2[0])
        m1_v2 = implict_dic[name]
    result.append(m1_v2)
    temp_lst1.clear()
    temp_lst2.clear()
    temp.clear()
# ----------------# v1_m2 =
    for i in range(len(lst1)):
        if (int(lst1_r[i]) > 3):
            temp_lst1.append(lst1_r[i])
            temp_lst2.append(lst2[i])
    if (len(temp_lst1) > 1):
        for i in range(len(temp_lst1)):
            name = str(temp_lst1[i]) + str(temp_lst2[i])
            temp.append(implict_dic[name])
        v1_m2 = sum(temp) / len(temp)
    else:
        name = str(temp_lst1[0]) + str(temp_lst2[0])
        v1_m2 = implict_dic[name]
    result.append(v1_m2)
    temp_lst1.clear()
    temp_lst2.clear()
    temp.clear()
# ----------------# v1_v2 =
    for i in range(len(lst1)):
        if (int(lst1_r[i]) > 3):
            temp_lst1.append(lst1_r[i])
            temp_lst2.append(lst2_r[i])
    if (len(temp_lst1) > 1):
        for i in range(len(temp_lst1)):
            name = str(temp_lst1[i]) + str(temp_lst2[i])
            temp.append(implict_dic[name])
        v1_v2 = sum(temp) / len(temp)
    else:
        name = str(temp_lst1[0]) + str(temp_lst2[0])
        v1_v2 = implict_dic[name]
    result.append(v1_v2)
    temp_lst1.clear()
    temp_lst2.clear()
    temp.clear()
# ----------------# m2_m1 =
    for i in range(len(lst1)):
        if (int(lst2[i]) > 3):
            temp_lst1.append(lst2[i])
            temp_lst2.append(lst1[i])
    if (len(temp_lst1) > 1):
        for i in range(len(temp_lst1)):
            name = str(temp_lst1[i]) + str(temp_lst2[i])
            temp.append(implict_dic[name])
        m2_m1 = sum(temp) / len(temp)
    else:
        name = str(temp_lst1[0]) + str(temp_lst2[0])
        m2_m1 = implict_dic[name]
    result.append(m2_m1)
    temp_lst1.clear()
    temp_lst2.clear()
    temp.clear()
# ----------------# m2_v1 =
    for i in range(len(lst1)):
        if (int(lst2[i]) > 3):
            temp_lst1.append(lst2[i])
            temp_lst2.append(lst1_r[i])
    if (len(temp_lst1) > 1):
        for i in range(len(temp_lst1)):
            name = str(temp_lst1[i]) + str(temp_lst2[i])
            temp.append(implict_dic[name])
        m2_v1 = sum(temp) / len(temp)
    else:
        name = str(temp_lst1[0]) + str(temp_lst2[0])
        m2_v1 = implict_dic[name]
    result.append(m2_v1)
    temp_lst1.clear()
    temp_lst2.clear()
    temp.clear()
# ----------------# v2_m1 =
    for i in range(len(lst1)):
        if (int(lst2_r[i]) > 3):
            temp_lst1.append(lst2_r[i])
            temp_lst2.append(lst1[i])
    if (len(temp_lst1) > 1):
        for i in range(len(temp_lst1)):
            name = str(temp_lst1[i]) + str(temp_lst2[i])
            temp.append(implict_dic[name])
        v2_m1 = sum(temp) / len(temp)
    else:
        name = str(temp_lst1[0]) + str(temp_lst2[0])
        v2_m1 = implict_dic[name]
    result.append(v2_m1)
    temp_lst1.clear()
    temp_lst2.clear()
    temp.clear()
# ----------------# v2_v1 =
    for i in range(len(lst1)):
        if (int(lst2_r[i]) > 3):
            temp_lst1.append(lst2_r[i])
            temp_lst2.append(lst1_r[i])
    if (len(temp_lst1) > 1):
        for i in range(len(temp_lst1)):
            name = str(temp_lst1[i]) + str(temp_lst2[i])
            temp.append(implict_dic[name])
        v2_v1 = sum(temp) / len(temp)
    else:
        name = str(temp_lst1[0]) + str(temp_lst2[0])
        v2_v1 = implict_dic[name]
    result.append(v2_v1)


    return result

# -----------------------------------------------------------------------------


def revers(Vj):
    Vjr = Vj.copy()
    for i in range(len(Vjr)):
        Vjr[i] = int(Vjr[i]) - 6
        if (int(Vjr[i]) < 0):
            Vjr[i] = int(Vjr[i]) * -1
    return Vjr

# -----------------------------------------------------------------------------

def PK(N, Pp, Vk, Vj):
    Vjr = revers(Vj)
    for i in range(len(Vjr)):
        Vjr[i] = int(Vjr[i]) - 6
        if(int(Vjr[i]) < 0):
            Vjr[i] = int(Vjr[i]) * -1
    sum1 = 0
    sum2 = 0
    for i in range(len(Vk)):
        s1 = int(Vk[i]) - int(Vj[i])
        s2 = int(Vk[i]) - int(Vjr[i])
        if(s1 < 0):
            s1 = s1 * -1
        sum1 += s1
        if(s2 < 0):
            s2 = s2 * -1
        sum2 += s2
    sum = sum2
    if(sum1 < sum2):
        sum = sum1
    res = 100 - (100 / (((N + 1) / 2) - 1)) * 1/Pp * sum
    return res

# -----------------------------------------------------------------------------

def PP(N, Pk, Vk, Vj):
    sum = 0
    for i in range(len(Vk)):
        s = int(Vk[i]) - int(Vj[i])
        if (s < 0):
            s = s * -1
        sum += s
    res = 100 - 100 / (N - 1) * 1/Pk * sum
    return res

#PP = podobnost' prvkov
plane_n_bus = PP(N, Pk, plane, bus)
plane_n_train = PP(N, Pk, plane, train)
plane_n_boat = PP(N, Pk, plane, boat)
bus_n_train = PP(N, Pk, bus, train)
bus_n_boat = PP(N, Pk, bus, boat)
train_n_boat = PP(N, Pk, train, boat)
print("| P O D O B N O S T   P R V K O V |")
print("| S I M I L A R I T Y  O F  E L E M E N T S |")
print()
print(" plane_n_bus = ", plane_n_bus)
print(" plane_n_train = ", plane_n_train)
print(" plane_n_boat = ", plane_n_boat)
print(" bus_n_train = ", bus_n_train)
print(" bus_n_boat = ", bus_n_boat)
print(" train_n_boat = ", train_n_boat)
print()
print("-----------------------------------------------------------------------------")
#PK = podobnost' konstruktov
k_r = PK(N, Pp, capacity, speed)
k_c = PK(N, Pp, capacity, price)
r_c = PK(N, Pp, speed, price)
print("| P O D O B N O S T   K O N S T R U K T O V |")
print ("| S I M I L A R I T Y  O F  C O N S T R U C T S |")
print()
print(" capacity - speed = ", k_r)
print(" capacity - price = ", k_c)
print(" speed - price = ", r_c)
print()
print("-----------------------------------------------------------------------------")
print(" | I M P L I K A T I A |")
print()
print(" Implication capacity - speed = ", implication(implict_dic, capacity, speed))
print(" Implication capacity - price = ", implication(implict_dic, capacity, price))
print(" Implication speed - price = ", implication(implict_dic, speed, price))

plane_rules = rules(D, n, N, W, plane)
bus_rules = rules(D, n, N, W, bus)
train_rules = rules(D, n, N, W, train)
boat_rules = rules(D, n, N, W, boat)

data = [plane_rules, bus_rules, train_rules, boat_rules]

cols = ('small capacity', 'large capacity', 'low speed', 'high speed', 'low price', 'high price')
rows = ['plane', 'bus', 'train', 'boat']

tab_vals = np.arange(-1, 1, 0.1)
val_incr = 1

colors = plt.cm.BuPu(np.linspace(0, 0.5, len(rows)))
n_rows = len(data)

index = np.arange(len(cols)) + 0.3
bar_width = 0.4

y_offset = np.zeros(len(cols))

# Plot bars and create text labels for the table
cell_text = []
for row in range(n_rows):
    plt.bar(index, data[row], bar_width, bottom=y_offset, color=colors[row])
    cell_text.append(["%0.2f" % x for x in data[row]])

# Add a table at the bottom of the axes
the_table = plt.table(cellText=cell_text,
                      rowLabels=rows,
                      colLabels=cols,
                      loc='bottom')

# Adjust layout to make room for the table:
plt.subplots_adjust(left=0.2, bottom=0.2)

plt.ylabel("Validity of the rule")
plt.yticks(tab_vals * val_incr, ['%0.1f' % val for val in tab_vals])
plt.xticks([])
plt.title('Rules')

plt.show()