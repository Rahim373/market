import { Menu, Layout } from 'antd';
import { useHistory, useLocation } from 'react-router-dom';
import navMenu from "./_nav";

const { SubMenu } = Menu;

function CreateMenu(items) {
  var menuItems = [];
  items.forEach(item => {
    if (item.children) {
      var children = CreateMenu(item.children);
      menuItems.push((<SubMenu key={item.key} icon={item.icon} title={item.name}>{children}</SubMenu>));
    } else {
      menuItems.push((<Menu.Item key={item.key} icon={item.icon} to={item.to} title={item.name}>{item.name}</Menu.Item>))
    }
  });
  return menuItems;
}

function AppSidebar() {
  const { Sider } = Layout;
  const menuItems = CreateMenu(navMenu);

  let history = useHistory();
  let location = useLocation();

  function handleMenuItemClick(e) {
    history.push(e.item.props.to);
  }

  let path = location.pathname;
  let setDefaultOpenMenus = [];
  let setDefaultMenuItem = [];
  if (path.startsWith('/')) {
    path = location.pathname.replace('/', '');
  }

  var splited = path.split('/');
  if (splited.length === 1) {
    setDefaultOpenMenus = [];
    setDefaultMenuItem = [splited[0]];
  } else if (splited.length > 1) {
    setDefaultOpenMenus = [splited[0]];
    setDefaultMenuItem = [splited[1]];
  }

  return (
    <Sider collapsible collapsed={false} style={{
      overflow: 'auto',
      height: '100vh',
      position: 'fixed',
      left: 0,
    }}>
      <div className="logo" />
      <Menu
        theme="dark"
        onClick={e => handleMenuItemClick(e)}
        mode="inline"
        defaultOpenKeys={setDefaultOpenMenus}
        defaultSelectedKeys={setDefaultMenuItem} >
        {menuItems}
      </Menu>
    </Sider>
  );
};

export default AppSidebar;