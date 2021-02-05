import request from "../../apis";

const CatalogService = {
    Categories: {
        GetAll: async (pageNumber = 1, PageLength = 15) => request.get(`/categories?pageNumber=${pageNumber}&pageLength=${PageLength}`),
        Detail: async (id) => request.get(`/categories/${id}`),
        Add: async (category) => request.post(`/categories`, category),
        Update: async (id, category) => request.put(`/categories/${id}`, category),
        Delete: async (id) => request.delete(`/categories/${id}`)
    }
};

export default CatalogService;