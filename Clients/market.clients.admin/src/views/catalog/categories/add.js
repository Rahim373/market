import { Col, Input, Row, Form, Button, Switch } from "antd";
import Title from "antd/lib/typography/Title";

function Add() {
    const layout = {
        labelCol: { span: 8 },
        wrapperCol: { span: 16 },
    };
    const tailLayout = {
        wrapperCol: { offset: 8, span: 16 },
    };

    const onFormSubmit = (values) => {
        console.log(values);
    }

    return (
        <>
            <Row>
                <Col span={24}>
                    <Title level={4}>Add Category</Title>
                </Col>
            </Row>

            <Row>
                <Col span={12}>
                    <Form {...layout}
                        requiredMark={'optional'}
                        initialValues={{ title: 'asd', slug: '', active: true }}
                        onFinish={onFormSubmit}>
                        <Form.Item
                            label="Category Title"
                            rules={[{ required: true, message: 'Please input category name!' }]}
                            name="title">
                            <Input />
                        </Form.Item>
                        <Form.Item
                            label="Slug"
                            name="slug"
                            extra="If slug is not provided, it will be generated automatically from the Title above">
                            <Input />
                        </Form.Item>
                        <Form.Item
                            label="Active"
                            required={true}
                            name="active"
                            extra="Default is Active">
                            <Switch defaultChecked={true} />
                        </Form.Item>
                        <Form.Item {...tailLayout}>
                            <Button type="primary" htmlType="submit">
                                Submit
                            </Button>
                        </Form.Item>
                    </Form>
                </Col>
            </Row>
        </>
    );
};

export default Add;