import pandas as pd
import requests

API_URL = "http://localhost:5185/api/inventory/bulk"

TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicGFyYXMiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTc3NjM0MDY0OCwiaXNzIjoid2FyZWhvdXNlLWFwaSIsImF1ZCI6IndhcmVob3VzZS11c2VycyJ9.st8_wWf-j6_slnGjib7nUuPmpwI3jECgkGZ36hkVP-Y"

headers = {
    "Authorization": f"Bearer {TOKEN}",
    "Content-Type": "application/json"
}

# Read CSV
df = pd.read_csv("data/inventory.csv")

# Fix column names (VERY IMPORTANT)
df = df.rename(columns={
    "ProductId": "productId",
    "WarehouseId": "warehouseId",
    "Quantity": "quantity"
})

# Convert to JSON payload
payload = df.to_dict(orient="records")

# Send bulk request
response = requests.post(API_URL, json=payload, headers=headers)

print("Status:", response.status_code)
print("Response:", response.text)