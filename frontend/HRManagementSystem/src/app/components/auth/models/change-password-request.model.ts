export class ChangePasswordRequestModel {
    userId: string;
    currentPassword: string;
    newPassword: string;
    confirmPassword: string;
}