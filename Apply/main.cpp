#include <Windows.h>


void ApplyPosition(HWND window) {
	SetWindowPos(window, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);
}

int main(int argc, char** argv) {
	HWND window = GetForegroundWindow();
	ApplyPosition(window);

	return 0;
}