const API_BASE_URL = "http://localhost:8080/api";

// Definiamo un tipo di errore personalizzato per gestire meglio gli errori API
export class ApiError extends Error {
    constructor(
        public status: number,
        public statusText: string,
        message?: string
    ) {
        super(message || "An API error occurred");
        this.name = "ApiError";
    }
}

/**
 * Funzione generica per effettuare richieste API.
 * Gestisce la costruzione dell'URL, l'aggiunta di header e il parsing della risposta.
 * @param endpoint L'endpoint API da chiamare (es. "/Product/GetProducts")
 * @param options Opzioni di Fetch (method, body, etc.)
 * @returns Una Promise che risolve con i dati della risposta in formato JSON.
 * @throws {ApiError} Se la risposta HTTP non è ok (es. status 4xx o 5xx).
 */
async function apiRequest<T>(
    endpoint: string,
    options: RequestInit = {}
): Promise<T> {
    const url = `${API_BASE_URL}${endpoint}`;

    const config: RequestInit = {
        ...options,
        headers: {
            "Content-Type": "application/json",
            ...options.headers,
        },
    };

    try {
        const response = await fetch(url, config);

        if (!response.ok) {
            // Se il server risponde con un errore, lanciamo un'eccezione
            const errorText = await response.text();
            throw new ApiError(
                response.status,
                response.statusText,
                errorText || `Request failed with status ${response.status}`
            );
        }

        // Se la risposta non ha contenuto (es. status 204), ritorniamo null
        if (response.status === 204) {
            return null as T;
        }

        return (await response.json()) as T;
    } catch (error) {
        if (error instanceof ApiError) {
            // Rialleciamo l'errore API per essere gestito a livello superiore
            throw error;
        }
        // Per altri errori (es. di rete), creiamo un errore generico
        console.error("Fetch API Error:", error);
        throw new Error(
            "Failed to fetch data from the server. Check your network connection."
        );
    }
}

export default apiRequest;
