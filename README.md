# Tubes3_StiMaTi

# Fingerprint-Based Biometric Identification Using Pattern Matching Algorithms

Tugas Besar 3 Mata Kuliah IF2211 Strategi Algoritma 2024 - Pemanfaatan Pattern Matching dalam Membangun Sistem Deteksi Individu Berbasis Biometrik Melalui Citra Sidik Jari

## Table of Contents
- [Description](#description)
- [Requirements and Installation](#requirements-and-installation)
- [How to Run](#how-to-run)
- [Features](#features)
- [Program Display](#program-display)
- [Author Information](#author-information)

## Description
This project implements fingerprint-based biometric identification using pattern matching algorithms such as KMP, Boyer-Moore (BM), and regular expressions.

### Algorithms
- **KMP (Knuth-Morris-Pratt)**: Efficiently searches for occurrences of a "pattern" within a "text" by preprocessing the pattern to determine the shifts.
- **Boyer-Moore (BM)**: Uses information gathered during the preprocessing of the pattern to skip sections of the text, making it faster for certain types of text.
- **Regular Expressions**: Utilized for complex pattern matching and text processing tasks.


## Requirements and Installation
- .NET SDK 6.0 or higher
- MySQL Server
- ImageSharp library
- MySQL.Data library

1. Install dependencies using the following command:
```
dotnet restore
```

2. Setup MySQL database:
```
mariadb -u root -p
CREATE DATABASE tubes3;
CREATE USER 'tubes3'@'localhost' IDENTIFIED BY 'stimati';
GRANT ALL PRIVILEGES ON tubes3.* TO 'tubes3'@'localhost';
FLUSH PRIVILEGES;
EXIT;
cd src/Database
mysqldump -u tubes3 -p tubes3 > tubes3_stima24.sql
```

## How to Run
To run the program, open a terminal or command prompt, navigate to the directory where the program file is saved, and run the following command:
```
cd src/Frontend/Frontend
dotnet run
```

## Features
- Pattern matching for biometric identification
- Easy to use with a GUI for interacting with the system.

## Program Display
![](src/Frontend/Frontend/img/bg_blocking.png)
![](assets/pic2.png)

## Author Information

Kelompok StiMaTi

| Name                    | NIM      |
| ----------------------- |:--------:|
| Raffael Boymian Siahaan | 13522046 |
| Nabila Shikoofa Muida   | 13522069 |
| Novelya Putri Ramadhani | 13522096 |