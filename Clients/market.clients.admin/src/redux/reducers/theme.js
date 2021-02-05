import { COLLAPSE_SIDEBAR } from "../actions/theme";

const initialState = {
    sidebarCollapsed: false
};

const themeReducer = (state = initialState, action) => {
    switch (action.type) {
        case COLLAPSE_SIDEBAR: {
            const { sidebarCollapsed } = action.payload;
            return {
                ...state,
                sidebarCollapsed: sidebarCollapsed
            };
        }
        default:
            return state;
    }
};

export default themeReducer;