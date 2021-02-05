export const COLLAPSE_SIDEBAR = "COLLAPSE_SIDEBAR";
export const SET_DEFAULT_MENU_ITEMS = "SET_DEFAULT_MENU_ITEMS";
export const SET_DEFAULT_OPEN_MENUS = "SET_DEFAULT_OPEN_MENUS";

export const collapeSidebar = collapse => ({
    type: COLLAPSE_SIDEBAR,
    payload: {
        sidebarCollapsed: collapse
    }
});

export const setDefaultMenuItem = (items = [])=> ({
    type: SET_DEFAULT_MENU_ITEMS,
    payload: {
        defaultMenuItems: items
    }
});

export const setDefaultOpenMenus = (items = [])=> ({
    type: SET_DEFAULT_OPEN_MENUS,
    payload: {
        defaultOpenMenus: items
    }
});