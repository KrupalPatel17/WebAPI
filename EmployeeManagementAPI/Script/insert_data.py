import json
import psycopg2

# ✅ Load JSON File
with open("countries+states+cities.json", "r", encoding="utf-8") as file:
    data = json.load(file)

# ✅ Connect to PostgreSQL Database
db = psycopg2.connect(
    host="localhost",
    user="postgres",  # Your PostgreSQL username
    password="123",   # Your PostgreSQL password
    database="empmanagementdb"
)
cursor = db.cursor()

# ✅ Insert Countries
for country in data:
    cursor.execute("""
        INSERT INTO countries (id, name, iso3, iso2, phonecode, capital, currency, currency_name, currency_symbol, region, subregion)
        VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)
        ON CONFLICT (id) DO UPDATE SET name=EXCLUDED.name
    """, (country["id"], country["name"], country["iso3"], country["iso2"], country["phonecode"],
          country["capital"], country["currency"], country["currency_name"], country["currency_symbol"],
          country["region"], country["subregion"]))

# ✅ Insert States
for country in data:
    for state in country["states"]:
        cursor.execute("""
            INSERT INTO states (id, name, state_code, latitude, longitude, country_id)
            VALUES (%s, %s, %s, %s, %s, %s)
            ON CONFLICT (id) DO UPDATE SET name=EXCLUDED.name
        """, (state["id"], state["name"], state["state_code"], state["latitude"], state["longitude"], country["id"]))

# ✅ Insert Cities
for country in data:
    for state in country["states"]:
        for city in state["cities"]:
            cursor.execute("""
                INSERT INTO cities (id, name, latitude, longitude, state_id)
                VALUES (%s, %s, %s, %s, %s)
                ON CONFLICT (id) DO UPDATE SET name=EXCLUDED.name
            """, (city["id"], city["name"], city["latitude"], city["longitude"], state["id"]))

db.commit()
db.close()
print("✅ Data inserted successfully!")
