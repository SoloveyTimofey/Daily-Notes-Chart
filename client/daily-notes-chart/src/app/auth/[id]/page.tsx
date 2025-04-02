interface IProps{
    params: {id: string};
}

export default function Page({params}: IProps) {

    const {id} = params;

    return (
        <div>auth/id {id}</div>
    );
}