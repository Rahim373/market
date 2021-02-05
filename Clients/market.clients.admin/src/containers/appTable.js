import { Table } from 'antd';
import React from 'react';

function AppTable(props) {
    return (
        <Table 
            columns={props.columns} 
            dataSource={props.data} 
            title={() => props.title}
            pagination={props.pagination}
            loading={props.loading}
            onChange={props.onChange}
            rowKey={records => records.id}></Table>
    )
}

export default AppTable;