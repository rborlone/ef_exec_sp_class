# Class for execute stored procedure from Entity FrameworkCore.

This class work with the entity framework pattern, and use the entity framework class for bring data of store procedure.

Inyect Dependency
```c#
private readonly DBConnection<ProveedorPalabraClave> _dbConnection;

        public ProveedorPalabraClaveRepository(DatabaseContext context)
        {
            _dbConnection = new DBConnection<ProveedorPalabraClave>(context);
        }
```

Example of use
```c#
    public void ActualizarPalabraClave(ProveedorPalabraClave palabraClave)
        {
            try
            {
                var list = new List<KeyValuePair<string, object>>() {
                    new KeyValuePair<string, object>("IdProveedorPalabraClave", palabraClave.@IdProveedorPalabraClave),
                    new KeyValuePair<string, object>("PalabraClave", palabraClave.PalabraClave),
                };

                _dbConnection.executeStoreProcedure("spma_ProveedorPalabraClave_Actualizar", list);
            }
            catch (DBConnectionException ex)
            {
                throw new DBConnectionException(ex.Message);
            }
        }
```