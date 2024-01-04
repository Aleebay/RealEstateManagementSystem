Imports System.IO

Module RealEstateManagementSystem
    ' Define a class to represent a property
    Class PropertyRecord
        Public Property ID As Integer
        Public Property Address As String
        Public Property PropertyType As String
        Public Property Price As Decimal
        Public Property Status As String
    End Class

    ' Define a list to store property records
    Dim Properties As New List(Of PropertyRecord)

    ' Specify the file paths for input and output files
    Dim InputFilePath As String = "C:\Users\aliab\Desktop\REMSInput.csv"
    Dim OutputFilePath As String = "C:\Users\aliab\Desktop\property_report.txt"

Sub Main()
    ' Load property data from the input file
    LoadPropertiesFromFile()

    ' Main program loop
    While True            
        ' Display menu options
        Console.WriteLine("Real Estate Management System")
        Console.WriteLine("1. Add Property")
        Console.WriteLine("2. Update Property")
        Console.WriteLine("3. Delete Property")
        Console.WriteLine("4. Search Properties")
        Console.WriteLine("5. Property Statistics")
        Console.WriteLine("6. Generate Reports")
        Console.WriteLine("7. Exit")
        Console.Write("Enter your choice: ")
        
        ' Read user input for menu choice
        Dim choice As Integer = Integer.Parse(Console.ReadLine())

        ' Process user's choice using a switch statement
        Select Case choice
            Case 1
                ' Call method to add new properties
                AddProperties()
            Case 2
                ' Call method to update existing property
                UpdateProperty()
            Case 3
                ' Call method to delete property
                DeleteProperty()
            Case 4
                ' Call method to search properties based on criteria
                SearchProperties()
            Case 5
                ' Call method to display property statistics
                PropertyStatistics()
            Case 6
                ' Call method to generate property reports
                GenerateReports()
            Case 7
                ' Call method to save properties to the output file and exit program
                SavePropertiesToFile()
                Exit Sub
            Case Else
                ' Invalid menu choice
                Console.WriteLine("Invalid choice. Please try again.")
        End Select

        Console.WriteLine() ' Add a blank line for spacing
    End While
End Sub

  ' Load property data from the input file
Sub LoadPropertiesFromFile()
    Try
        ' Open and read data from the input file
        Using reader As New StreamReader(InputFilePath) ' Open the input file for reading
            ' Loop through each line in the file
            While Not reader.EndOfStream
                ' Read a line and split its values using tab as separator
                Dim line As String = reader.ReadLine()
                Dim values As String() = line.Split(vbTab)
                
                ' Check if the line contains complete property information
                If values.Length = 5 Then
                    ' Create a new PropertyRecord object and populate its properties
                    Dim prop As New PropertyRecord()
                    prop.ID = Integer.Parse(values(0))
                    prop.Address = values(1)
                    prop.PropertyType = values(2)
                    prop.Price = Decimal.Parse(values(3))
                    prop.Status = values(4)
                    
                    ' Add the property record to the Properties list
                    Properties.Add(prop)
                End If
            End While
        End Using
    Catch ex As Exception
        ' Handle exceptions if an error occurs during loading
        Console.WriteLine("Error loading property data: " & ex.Message)
    End Try
End Sub


  ' Save property data to a file
Sub SavePropertiesToFile()
    Try
        ' Open the output file for writing and create a StreamWriter
        Using writer As New StreamWriter(OutputFilePath) ' Open the output file for writing
            ' Loop through each property record in the Properties list
            For Each prop In Properties
                ' Write the property information to the output file, separated by tabs
                writer.WriteLine($"{prop.ID}{vbTab}{prop.Address}{vbTab}{prop.PropertyType}{vbTab}{prop.Price}{vbTab}{prop.Status}")
            Next
        End Using
    Catch ex As Exception
        ' Handle exceptions if an error occurs during saving
        Console.WriteLine("Error saving property data: " & ex.Message)
    End Try
End Sub

    
   ' Add properties
Sub AddProperties()
    Console.WriteLine("Add Properties")
    
    ' Loop to allow adding multiple properties
    While True
        ' Prompt user to enter property details
        Console.Write("Enter Address (or press Enter to finish): ")
        Dim address As String = Console.ReadLine()
        
        ' Check if user wants to finish adding properties
        If String.IsNullOrEmpty(address) Then
            Exit While ' Exit the loop if address is empty
        End If
        
        Console.Write("Enter Property Type: ")
        Dim propertyType As String = Console.ReadLine()
        
        Console.Write("Enter Price: ")
        Dim price As Decimal = Decimal.Parse(Console.ReadLine())
        
        Console.Write("Enter Status: ")
        Dim status As String = Console.ReadLine()

        ' Generate a new ID for the property
        Dim newID As Integer = If(Properties.Any(), Properties.Max(Function(prop) prop.ID) + 1, 1)
        
        ' Create a new PropertyRecord object with entered details
        Dim newProperty As New PropertyRecord With {
            .ID = newID,
            .Address = address,
            .PropertyType = propertyType,
            .Price = price,
            .Status = status
        }

        ' Add the new property to the Properties list
        Properties.Add(newProperty)
        Console.WriteLine("Property added successfully.")
    End While
End Sub

   ' Update an existing property
Sub UpdateProperty()
    Console.Write("Enter Property ID to update: ")
    Dim idToUpdate As Integer = Integer.Parse(Console.ReadLine())
    
    ' Find the property to update using the provided ID
    Dim propertyToUpdate As PropertyRecord = Properties.FirstOrDefault(Function(prop) prop.ID = idToUpdate)

    ' Check if the property exists
    If propertyToUpdate IsNot Nothing Then
        ' Prompt user for new property details
        Console.Write("Enter new Address: ")
        propertyToUpdate.Address = Console.ReadLine()
        
        Console.Write("Enter new Property Type: ")
        propertyToUpdate.PropertyType = Console.ReadLine()
        
        Console.Write("Enter new Price: ")
        propertyToUpdate.Price = Decimal.Parse(Console.ReadLine())
        
        Console.Write("Enter new Status: ")
        propertyToUpdate.Status = Console.ReadLine()
        
        Console.WriteLine("Property updated successfully.")
    Else
        ' Property not found
        Console.WriteLine("Property not found.")
    End If
End Sub
' Delete a property
Sub DeleteProperty()
    Console.Write("Enter Property ID to delete: ")
    Dim idToDelete As Integer = Integer.Parse(Console.ReadLine())
    
    ' Find the property to delete using the provided ID
    Dim propertyToDelete As PropertyRecord = Properties.FirstOrDefault(Function(prop) prop.ID = idToDelete)

    ' Check if the property exists
    If propertyToDelete IsNot Nothing Then
        ' Remove the property from the Properties list
        Properties.Remove(propertyToDelete)
        Console.WriteLine("Property deleted successfully.")
    Else
        ' Property not found
        Console.WriteLine("Property not found.")
    End If
End Sub

    ' Search properties based on criteria
Sub SearchProperties()
    Console.WriteLine("Search Properties")
    Console.WriteLine("1. By Property Type")
    Console.WriteLine("2. By Price Range")
    Console.WriteLine("3. By Status")
    Console.Write("Enter your choice: ")
    Dim searchChoice As Integer = Integer.Parse(Console.ReadLine())

    ' Based on user's search choice, perform property search
    Select Case searchChoice
        Case 1
            ' Search by Property Type
            Console.Write("Enter Property Type: ")
            Dim propertyType As String = Console.ReadLine()
            
            ' Filter properties based on specified property type
            Dim results = Properties.Where(Function(prop) prop.PropertyType.Equals(propertyType, StringComparison.OrdinalIgnoreCase))
            
            ' Display search results
            DisplaySearchResults(results)
            
        Case 2
            ' Search by Price Range
            Console.Write("Enter Minimum Price: ")
            Dim minPrice As Decimal = Decimal.Parse(Console.ReadLine())
            
            Console.Write("Enter Maximum Price: ")
            Dim maxPrice As Decimal = Decimal.Parse(Console.ReadLine())
            
            ' Filter properties based on specified price range
            Dim results = Properties.Where(Function(prop) prop.Price >= minPrice AndAlso prop.Price <= maxPrice)
            
            ' Display search results
            DisplaySearchResults(results)
            
        Case 3
            ' Search by Status
            Console.Write("Enter Status: ")
            Dim status As String = Console.ReadLine()
            
            ' Filter properties based on specified status
            Dim results = Properties.Where(Function(prop) prop.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
            
            ' Display search results
            DisplaySearchResults(results)
            
        Case Else
            ' Invalid search choice
            Console.WriteLine("Invalid choice.")
    End Select
End Sub
' Display search results
Sub DisplaySearchResults(results As IEnumerable(Of PropertyRecord))
    Console.WriteLine("Search Results:")
    
    ' Check if there are search results
    If results.Any() Then
        ' Loop through each property in the search results
        For Each prop In results
            ' Display property details in a formatted manner
            Console.WriteLine($"ID: {prop.ID}, Address: {prop.Address}, Property Type: {prop.PropertyType}, Price: {prop.Price:C}, Status: {prop.Status}")
        Next
    Else
        ' No search results found
        Console.WriteLine("No properties found.")
    End If
End Sub

' Calculate and display property statistics
Sub PropertyStatistics()
    Console.WriteLine("Property Statistics")
    
    ' Display total number of properties
    Console.WriteLine($"Total Properties: {Properties.Count}")
    
    ' Calculate and display average property price
    Console.WriteLine($"Average Price: {Properties.Average(Function(prop) prop.Price):C}")
    
    ' Calculate and display property type distribution
    Dim propertyTypes = Properties.GroupBy(Function(prop) prop.PropertyType).Select(Function(group) New With {.Type = group.Key, .Count = group.Count()})
    Console.WriteLine("Property Type Distribution:")
    For Each type In propertyTypes
        ' Display property type and its count
        Console.WriteLine($"{type.Type}: {type.Count}")
    Next
End Sub
' Generate property reports and save to the output file
Sub GenerateReports()
    Try
        ' Open the output file for writing and create a StreamWriter
        Using writer As New StreamWriter(OutputFilePath) ' Open the output file for writing
            ' Write the list of available properties section header
            writer.WriteLine("List of Available Properties")
            writer.WriteLine() ' Add a blank line
            
            ' Write column headers for available properties
            writer.WriteLine($"{"ID",-6}{"Address",-20}{"Property Type",-15}{"Price",-10}{"Status",-10}")
            writer.WriteLine(New String("-", 60)) ' Add a line separator
            
            ' Loop through each property to write available properties
            For Each prop In Properties
                ' Check if the property is for sale
                If prop.Status.Equals("For Sale", StringComparison.OrdinalIgnoreCase) Then
                    ' Write property details in a formatted manner
                    writer.WriteLine($"{prop.ID,-6}{prop.Address,-20}{prop.PropertyType,-15}{prop.Price,-10:C}{prop.Status,-10}")
                End If
            Next

            writer.WriteLine() ' Add a blank line
            
            ' Write list of sold properties section header
            writer.WriteLine("List of Sold Properties")
            writer.WriteLine() ' Add a blank line
            
            ' Write column headers for sold properties
            writer.WriteLine($"{"ID",-6}{"Address",-20}{"Property Type",-15}{"Price",-10}{"Status",-10}")
            writer.WriteLine(New String("-", 60)) ' Add a line separator
            
            ' Loop through each property to write sold properties
            For Each prop In Properties
                ' Check if the property is sold
                If prop.Status.Equals("Sold", StringComparison.OrdinalIgnoreCase) Then
                    ' Write property details in a formatted manner
                    writer.WriteLine($"{prop.ID,-6}{prop.Address,-20}{prop.PropertyType,-15}{prop.Price,-10:C}{prop.Status,-10}")
                End If
            Next
        End Using
        
        Console.WriteLine("Reports generated and saved to output.txt")
    Catch ex As Exception
        ' Handle exceptions if an error occurs during report generation
        Console.WriteLine("Error generating reports: " & ex.Message)
    End Try
End Sub
End Module
