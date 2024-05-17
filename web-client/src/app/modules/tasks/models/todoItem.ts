export class TodoItem
{
    id?: string;
    task: string;
    deadline: Date;
    details: string;
    isComplete: boolean;
    subItem?: TodoItem[];
}
