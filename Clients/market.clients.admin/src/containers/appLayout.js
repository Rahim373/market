import { Layout } from 'antd';
import MainContent from './appContent';
import Sidebar from './appSidebar';
import '../styles/appLayout.css';
import AppFooter from './appFooter';
import AppHeader from './appHeader';

const { Content } = Layout;

function AppLayout() {
    return (
        <Layout>
            <Sidebar />
            <Layout className="site-layout" style={{ marginLeft: 200 }}>
                <AppHeader></AppHeader>
                <Content
                    className="site-layout-background"
                    style={{
                    margin: '24px 16px',
                    padding: 24,
                    minHeight: 280,
                    }}
                >
                    <MainContent />
                </Content>
                <AppFooter></AppFooter>
            </Layout>
      </Layout>
    );
}

export default AppLayout;