# CSV to VCF Converter

A small Windows console tool that converts a contacts **CSV file exported from Outlook** into a single **vCard (`.vcf`)** file. You can then import that file into a phone, Gmail, iCloud or Outlook.

## How to use

1. **Export your contacts from Outlook:** File > Open & Export > Import/Export > *Export to a file* > *Comma Separated Values* > choose **Contacts**.
2. **Run `CsvToVcf.exe`** and answer two questions:
   - the full path of the CSV file
   - a name for the output file, without the extension
3. **Find the result:** `<name>.vcf` is written next to the CSV file.

## What it converts

The converter reads all 61 columns of Outlook's CSV export. It writes a vCard 3.0 entry for each contact, with:
- name and nickname
- emails
- home, work and mobile phones, and fax numbers
- company and job title
- home, work and other addresses
- web pages and birthday
- notes

## Limitations

- The CSV must be in **Outlook's export format**: the same columns, in the same order.
- Lines are split on commas, so a value that contains a comma (even inside quotes) shifts the columns. Check the result if your contacts have commas in names or notes.

## Requirements

- Windows 10 or 11
- .NET Framework 4.8 (already included in Windows 10 1903 and later, and in Windows 11)

## Build

```powershell
dotnet build CsvToVcf/CsvToVcf.csproj -c Release
```
