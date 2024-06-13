import csv

def extract_last_two_digits(object_id):
    # Extract the last two digits of the object ID
    return object_id[-2:]

def reformat_trace_data(input_file):
    # Read input data from the text file
    with open(input_file, 'r') as file:
        input_data = file.read()

    # Split the input data into lines
    lines = input_data.strip().split('\n')

    # Extract object IDs from each line and keep only the last four digits
    object_ids = []
    for line in lines:
        parts = line.split()
        if len(parts) >= 4:
            object_ids.append(extract_last_two_digits(parts[3]))

    # Create pairs of current and next object IDs
    pairs = [(object_ids[i], object_ids[i+1]) for i in range(len(object_ids) - 1)]

    # Write the pairs to a CSV file
    with open('output.csv', 'w', newline='') as csvfile:
        csv_writer = csv.writer(csvfile)
        csv_writer.writerow(['Current Object ID', 'Next Object ID'])
        csv_writer.writerows(pairs)

    print("CSV file 'output.csv' has been created successfully.")

# Specify the input file containing the trace data
input_file = 'IBMObjectStoreTrace025Part0.txt'

reformat_trace_data(input_file)
