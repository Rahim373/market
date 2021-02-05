import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import AppTable from '../../../containers/appTable';
import CatalogService from '../catalogService';

function CategoryList() {
    const [data, setData] = useState([]);
    const [pagination, setPagination] = useState({
        current: 1,
        pageSize: 10
    });
    const [loading, setloading] = useState(true);

    const onTableChange = (e) => {
        setPagination({
            ...pagination,
            current: e.current,
            pageSize: e.pageSize
        });
    }

    useEffect(async () => {
        const getData = async () => {
            setloading(true);
            var response = await CatalogService.Categories.GetAll(pagination.current, pagination.pageSize);
            setloading(false);
            setData(response.entity.items);
            setPagination({
                ...pagination,
                total: response.entity.totalCount
            });
        };

        await getData();
    }, [pagination.current, pagination.pageSize]);

    const cols = [
        {
            title: 'Title',
            dataIndex: 'title',
            key: 'name',
        }
    ];

    return (
        <>
        <Link to="/catalog/categories/add" title="Add">Add</Link>
        
        <AppTable
            columns={cols}
            data={data}
            title={'Categories'}
            pagination={pagination}
            loading={loading}
            onChange={e => onTableChange(e)}
            rowKey={record => record.id} />
        </>
    );
};

export default CategoryList;