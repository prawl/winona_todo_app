export class TodoItem
{
    id: number;
    task: string;
    deadline: Date;
    details: string;
    isComplete: boolean;
    subItem: TodoItem;
}
