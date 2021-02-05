import {DashboardOutlined, BuildOutlined, AppstoreOutlined} from '@ant-design/icons';

const navMenu = [
    { key: 'dashboard', name: 'Dashboard', to: '/dashboard', icon: <DashboardOutlined /> },
    { key: 'catalog', name: 'Catalog', to: '/catalog', icon: <BuildOutlined />, children: [
        { key: 'categories', name: 'Categories', to: '/catalog/categories', icon: <AppstoreOutlined />},
        { key: 'products', name: 'Products', to: '/catalog/products', icon: <AppstoreOutlined />}
    ]},
];

export default navMenu;