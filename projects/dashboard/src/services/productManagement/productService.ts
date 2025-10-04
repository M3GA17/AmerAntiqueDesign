import apiRequest from "../apiService";
import type { Product } from "@/types/productManagement/product";

const productService = {
    /**
     * Recupera la lista di tutti i prodotti.
     * @returns Una Promise che risolve con un array di prodotti.
     */
    getProducts: (): Promise<Product[]> => {
        // Nota: l'URL corretto è /Product/GetProducts, rispettando il case del controller C#
        return apiRequest<Product[]>("/Product/GetProducts", {
            method: "GET",
        });
    },

    // Qui potrai aggiungere altre funzioni come:
    // getProductById: (id: string): Promise<Product> => { ... },
    // createProduct: (productData: Omit<Product, 'id'>): Promise<Product> => { ... },
    // updateProduct: (id: string, productData: Partial<Product>): Promise<Product> => { ... },
    // deleteProduct: (id: string): Promise<void> => { ... },
};

export default productService;
