import React, { useEffect } from 'react';
import API from '../../../apis';
import AppTable from '../../../containers/appTable';

function Products() {
    useEffect(async ()=>{
        var data = await API.get('products');
        console.log(data.data);
    }, []);




    const data = React.useMemo(
        () => [
            {
                "name": "A"
            },
            {
                "name": "B"
            }
        ], []
    );

    const cols = [{
        title: 'Name',
        dataIndex: 'name',
        sorter: (a, b) => a.name.localeCompare(b.name) 
    }];

    return (
        <AppTable columns={cols} data={data} title={'Products'} />
    );
};

export default Products;