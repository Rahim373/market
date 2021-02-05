export const LOAD_CATEGORIES = "LOAD_CATEGORIES";

export const loadCategories = (pageNumber = 1, pageLength = 15) => ({
    type: LOAD_CATEGORIES,
    payload: {
        pageNumber,
        pageLength
    }
});